using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P0_ChrisSophiea
{
    class Program
    {
        static DAOMethodsImpl db = new DAOMethodsImpl();
        static Validation validation = new Validation();
        static void Main(string[] args)
        {
            int mainMenuSelection;
            int categorySelection;
            string shopSelection;
            Customer user;
            Store store = new Store();
            Purchase purchase1 = new Purchase();
            ICollection<Inventory> inventory;
            Dictionary<Item, int> cart;
            double total = 0;

            Console.WriteLine("\n--- Welcome to the Official Funcoland Store ---");
            //initializeData();
            do
            {
                purchase1 = new Purchase();
                mainMenuSelection = validation.vMainMenu();
                if (mainMenuSelection == 2)
                {
                    db.PrintAllCustomers();
                    continue;
                }
                else if (mainMenuSelection == 3) { break; }

                user = validation.vLoginMenu();
                user = db.AddCustomer(user);

                do
                {
                    store = null;
                    int customerResponse = validation.vCustomerMenu();

                    if (customerResponse == 1)
                    {
                        foreach (Purchase p in db.GetPurchasesByCustomer(user))
                        {
                            Console.WriteLine($"\n{p}");
                        }
                        continue;
                    }
                    else if (customerResponse == 3) { break; }


                    purchase1 = new Purchase();
                    inventory = new List<Inventory>();
                    store = validation.vStoreMenu();
                    cart = new Dictionary<Item, int>();
                    total = 0;
                    //purchase = null;
                    if (store.StoreAddress == null) { break; }

                    do
                    {
                        categorySelection = validation.vItemCategoryMenu();


                        if (categorySelection == 1)
                        {
                            inventory = db.GetInventory(store);
                            db.PrintAllInventory(store);
                        }
                        else if (categorySelection == 2)
                        {
                            inventory = db.GetInventoryByType(store, "Console");
                            db.PrintInventoryByType(store, "Console");
                        }
                        else if (categorySelection == 3)
                        {
                            inventory = db.GetInventoryByType(store, "Game");
                            db.PrintInventoryByType(store, "Game");
                        }
                        else if (categorySelection == 4)
                        {
                            inventory = db.GetInventoryByType(store, "Accessory");
                            db.PrintInventoryByType(store, "Accessory");
                        }
                        else if (categorySelection == 5)
                        {
                            foreach (Purchase p in db.GetPurchasesByStore(store))
                            {
                                Console.WriteLine($"\n{p}");
                            }
                            continue;
                        }
                        else
                        {
                            store = null;
                            break;
                        }
                        do
                        {

                            shopSelection = null;
                            shopSelection = validation.vShopMenu(inventory);
                            if (shopSelection.Equals("back", StringComparison.OrdinalIgnoreCase)) { break; }
                            else if (shopSelection.Equals("check out", StringComparison.OrdinalIgnoreCase))
                            {

                                purchase1 = new Purchase();
                                purchase1.Customer1Id = user.CustomerId;
                                purchase1.Store1Id = store.StoreId;
                                purchase1.Item1 = cart.Keys;
                                purchase1.PurchaseDate = DateTime.Now;
                                purchase1.TotalPrice = total;
                                Console.WriteLine("\nYour purchase is completed: ");
                                foreach (Item i in cart.Keys)
                                {
                                    Console.Write($"\n{i} x {cart[i]}");
                                }
                                Console.WriteLine($"\n\nTotal: {total}");
                                db.AddPurchase(purchase1);
                                cart = new Dictionary<Item, int>();
                                total = 0;
                                break;
                            }


                            Inventory inventory1 = db.GetInventoryById(int.Parse(shopSelection));
                            Console.WriteLine($"\nYou selected: {inventory1.Item1.ItemName} | Price: {inventory1.Item1.ItemPrice} | In Stock: {inventory1.InventoryAmount}");
                            int quantity = 0;
                            do
                            {
                                Console.Write("\n\tHow many do you want? Enter valid quantity: ");
                                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0 || quantity > inventory1.InventoryAmount)
                                {
                                    Console.WriteLine("Invalid quantity entered. Try again.");
                                }
                                else
                                {
                                    db.ReduceInventory(inventory1, quantity);
                                    cart.Add(inventory1.Item1, quantity);
                                    total += inventory1.Item1.ItemPrice * quantity;
                                    break;
                                }

                            } while (quantity < 0 || quantity > inventory1.InventoryAmount);


                        } while (!shopSelection.Equals("back", StringComparison.OrdinalIgnoreCase));




                    } while (categorySelection != 6);

                } while (store == null);

            } while (mainMenuSelection != 3);









        }
        protected static void initializeData()
        {
            //create items
            Item n64 = new Item()
            {
                ItemName = "Nintendo 64",
                ItemType = "Console",
                ItemDescription = "A home video game console developed and marketed by Nintendo. Named for its 64-bit central processing unit, it was released on September 29, 1996 in North America.",
                ItemPrice = 250.00
            };
            db.AddItem(n64);

            Item saturn = new Item()
            {
                ItemName = "Sega Saturn",
                ItemType = "Console",
                ItemDescription = "A home video game console developed by Sega and released on May 11, 1995 in North America.It is the successor to the successful Sega Genesis.",

                ItemPrice = 179.99
            };
            db.AddItem(saturn);

            Item playstion = new Item()
            {
                ItemName = "Sony PlayStation",
                ItemType = "Console",
                ItemDescription = "A home video game console developed and marketed by Sony Computer Entertainment. It was first released on September 9, 1995 in North America. The PlayStation is Sony's introduction into the video-game market.",
                ItemPrice = 199.99
            };
            db.AddItem(playstion);


            Item mario = new Item()
            {
                ItemName = "Mario 64",
                ItemType = "Game",
                ItemDescription = "A platform game and the first in the Super Mario series to feature 3D gameplay. As Mario, the player explores Princess Peach's castle and must rescue her from Bowser.",
                ItemPrice = 60
            };
            db.AddItem(mario);

            Item goldeneye = new Item()
            {
                ItemName = "GoldenEye 007",
                ItemType = "Game",
                ItemDescription = "A first-person shooter developed by Rare. Assume the role of James Bond as he fights to prevent a criminal syndicate from using a satellite weapon against London to cause a global financial meltdown. The game includes a split-screen multiplayer with up to four players.",
                ItemPrice = 55
            };
            db.AddItem(goldeneye);

            Item zelda = new Item()
            {
                ItemName = "Legend of Zelda: Ocarina of Time",
                ItemType = "Game",
                ItemDescription = "An Action-Adventure game developed by Nintendo. The player controls Link in the fantasy land of Hyrule on a quest to stop the evil king Ganondorf, by traveling through time and navigating dungeons and an overworld.",
                ItemPrice = 60
            };
            db.AddItem(zelda);

            Item guardian = new Item()
            {
                ItemName = "Guardian Heroes",
                ItemType = "Game",
                ItemDescription = "A 2D side-scrolling beat 'em up video game with RPG elements developed by Treasure. The game allows players to alter the storyline through their actions, leading to multiple endings.",
                ItemPrice = 70
            };
            db.AddItem(guardian);

            Item radiant = new Item()
            {
                ItemName = "Radiant Silvergun",
                ItemType = "Game",
                ItemDescription = "A shoot 'em up developed by Treasure. The story follows a team of fighter pilots in the far future who are battling waves of enemies summoned by a mysterious crystal dug up from the Earth. The player hosts an arsenal of six different types of shots to choose from, and a sword to destroy nearby targets.",
                ItemPrice = 65
            };
            db.AddItem(radiant);

            Item nights = new Item()
            {
                ItemName = "Nights Into Dreams",
                ItemType = "Game",
                ItemDescription = "An action game developed by Sonic Team. The story follows teenagers Elliot Edwards and Claris Sinclair, who enter Nightopia, a dream world where all dreams take place. With the help of Nights, an exiled Nightmaren, they begin a journey to stop the evil ruler Wizeman from destroying Nightopia and consequently the real world.",
                ItemPrice = 55
            };
            db.AddItem(nights);

            Item mgs = new Item()
            {
                ItemName = "Metal Gear Solid",
                ItemType = "Game",
                ItemDescription = "A stealth game developed by Konami. Players control Solid Snake, a soldier who infiltrates a nuclear weapons facility to neutralize the terrorist threat from FOXHOUND, a renegade special forces unit.",
                ItemPrice = 50
            };
            db.AddItem(mgs);

            Item re2 = new Item()
            {
                ItemName = "Resident Evil 2",
                ItemType = "Game",
                ItemDescription = "A survival horror game developed and published by Capcom. The player controls Leon S. Kennedy and Claire Redfield, who must escape Raccoon City after its citizens are transformed into zombies by a biological weapon two months after the events of the original Resident Evil.",
                ItemPrice = 50
            };
            db.AddItem(re2);

            Item ff7 = new Item()
            {
                ItemName = "Final Fantasy 7",
                ItemType = "Game",
                ItemDescription = "A role-playing video game developed by Square. It is the seventh main installment in the Final Fantasy series. The game's story follows Cloud Strife, a mercenary who joins an eco-terrorist organization to stop a world-controlling megacorporation from using the planet's life essence as an energy source.",
                ItemPrice = 60
            };
            db.AddItem(ff7);

            Item rumblepak = new Item()
            {
                ItemName = "N64 Rumble Pak",
                ItemType = "Accessory",
                ItemDescription = "Rumble pack that allows your controller to rumble during gameplay.",
                ItemPrice = 40
            };
            db.AddItem(rumblepak);

            Item n64cont = new Item()
            {
                ItemName = "N64 Controller - Atomic Purple",
                ItemType = "Accessory",
                ItemDescription = "N64 Controller in translucent purple shell.",
                ItemPrice = 45
            };
            db.AddItem(n64cont);

            Item n64expansion = new Item()
            {
                ItemName = "N64 Expansion Pak",
                ItemType = "Accessory",
                ItemDescription = "Allows the random access memory (RAM) of the Nintendo 64 console to increase from 4 megabytes (MB) to 8 MB.",
                ItemPrice = 70
            };
            db.AddItem(n64expansion);

            Item n64mem = new Item()
            {
                ItemName = "Nintendo 64 Controller Pak",
                ItemType = "Accessory",
                ItemDescription = "N64 Memory Card. Plugs into the back of the N64 controller",
                ItemPrice = 35
            };
            db.AddItem(n64mem);

            Item saturncont = new Item()
            {
                ItemName = "Sega Saturn Controller",
                ItemType = "Accessory",
                ItemDescription = "Original Sega Saturn controller",
                ItemPrice = 40
            };
            db.AddItem(saturncont);

            Item saturn3d = new Item()
            {
                ItemName = "Sega Saturn 3D Controller",
                ItemType = "Accessory",
                ItemDescription = "Sega Saturn Controller with added joystick and analog shoulder triggers",
                ItemPrice = 50
            };
            db.AddItem(saturn3d);

            Item saturngun = new Item()
            {
                ItemName = "Sega Saturn Stunner Light Gun",
                ItemType = "Accessory",
                ItemDescription = "Light gun compatible with Sega Saturn",
                ItemPrice = 55
            };
            db.AddItem(saturngun);

            Item psds = new Item()
            {
                ItemName = "PlayStation DualShock Controller - Original Grey",
                ItemType = "Accessory",
                ItemDescription = "DualShock controller adds 2 joysticks to the original PlayStation controller.",
                ItemPrice = 50
            };
            db.AddItem(psds);

            Item psdsw = new Item()
            {
                ItemName = "PlayStation DualShock Controller - White",
                ItemType = "Accessory",
                ItemDescription = "DualShock controller adds 2 joysticks to the original PlayStation controller. Color is white.",
                ItemPrice = 55
            };
            db.AddItem(psdsw);

            Item psmulti = new Item()
            {
                ItemName = "PlayStation Multitap",
                ItemType = "Accessory",
                ItemDescription = "Expands the number of controllers and memory cards that can be plugged into the PlayStation from 2 to 5.",
                ItemPrice = 50
            };
            db.AddItem(psmulti);

            Item psmem = new Item()
            {
                ItemName = "PlayStation Memory Card",
                ItemType = "Accessory",
                ItemDescription = "Allows you to save the state of your games. Holds up to 1MB of game saves.",
                ItemPrice = 25
            };
            db.AddItem(psmem);

            //ADD STORES
            Store s1 = new Store()
            {
                StoreAddress = "123 N Fake St, Austin, TX",
                PhoneNumber = "123-555-4567"
            };
            db.AddStore(s1);

            db.AddInventory(s1, n64, 8);
            db.AddInventory(s1, saturn, 3);
            db.AddInventory(s1, playstion, 4);
            db.AddInventory(s1, zelda, 7);
            db.AddInventory(s1, mario, 5);
            db.AddInventory(s1, goldeneye, 4);
            db.AddInventory(s1, re2, 4);
            db.AddInventory(s1, mgs, 6);
            db.AddInventory(s1, ff7, 2);
            db.AddInventory(s1, radiant, 1);
            db.AddInventory(s1, nights, 3);
            db.AddInventory(s1, n64cont, 5);
            db.AddInventory(s1, n64expansion, 4);
            db.AddInventory(s1, rumblepak, 2);
            db.AddInventory(s1, n64mem, 2);
            db.AddInventory(s1, psds, 2);
            db.AddInventory(s1, psdsw, 4);
            db.AddInventory(s1, psmulti, 2);
            db.AddInventory(s1, psmem, 7);
            db.AddInventory(s1, saturncont, 4);
            db.AddInventory(s1, saturngun, 1);



            Store s2 = new Store()
            {
                StoreAddress = "4560 W Lambda Dr, Los Angeles, CA",
                PhoneNumber = "567-555-8901"
            };
            db.AddStore(s2);

            db.AddInventory(s2, n64, 5);
            db.AddInventory(s2, saturn, 2);
            db.AddInventory(s2, playstion, 6);
            db.AddInventory(s2, zelda, 4);
            db.AddInventory(s2, mario, 5);
            db.AddInventory(s2, goldeneye, 3);
            db.AddInventory(s2, re2, 5);
            db.AddInventory(s2, mgs, 2);
            db.AddInventory(s2, ff7, 4);
            db.AddInventory(s2, guardian, 2);
            db.AddInventory(s2, radiant, 1);
            db.AddInventory(s2, nights, 1);
            db.AddInventory(s2, n64cont, 7);
            db.AddInventory(s2, n64expansion, 3);
            db.AddInventory(s2, rumblepak, 5);
            db.AddInventory(s2, n64mem, 3);
            db.AddInventory(s2, psds, 5);
            db.AddInventory(s2, psdsw, 3);
            db.AddInventory(s2, psmulti, 3);
            db.AddInventory(s2, psmem, 8);
            db.AddInventory(s2, saturncont, 4);
            db.AddInventory(s2, saturn3d, 3);
            db.AddInventory(s2, saturngun, 2);

            Store s3 = new Store()
            {
                StoreAddress = "6618 N 21st St, Chicago, IL",
                PhoneNumber = "890-555-1234"
            };
            db.AddStore(s3);

            db.AddInventory(s3, n64, 7);
            db.AddInventory(s3, playstion, 6);
            db.AddInventory(s3, zelda, 9);
            db.AddInventory(s3, mario, 8);
            db.AddInventory(s3, goldeneye, 5);
            db.AddInventory(s3, re2, 3);
            db.AddInventory(s3, mgs, 7);
            db.AddInventory(s3, ff7, 2);
            db.AddInventory(s3, guardian, 2);
            db.AddInventory(s3, radiant, 4);
            db.AddInventory(s3, nights, 6);
            db.AddInventory(s3, n64cont, 5);
            db.AddInventory(s3, n64expansion, 2);
            db.AddInventory(s3, rumblepak, 7);
            db.AddInventory(s3, n64mem, 4);
            db.AddInventory(s3, psds, 4);
            db.AddInventory(s3, psdsw, 3);
            db.AddInventory(s3, psmulti, 2);
            db.AddInventory(s3, psmem, 6);
            db.AddInventory(s3, saturncont, 2);
            db.AddInventory(s3, saturn3d, 4);
            db.AddInventory(s3, saturngun, 1);

            Store s4 = new Store()
            {
                StoreAddress = "910 E Burr Ave, Boston, MA",
                PhoneNumber = "340-555-5698"
            };
            db.AddStore(s4);

            db.AddInventory(s4, n64, 4);
            db.AddInventory(s4, playstion, 5);
            db.AddInventory(s4, saturn, 3);
            db.AddInventory(s4, zelda, 5);
            db.AddInventory(s4, mario, 7);
            db.AddInventory(s4, goldeneye, 5);
            db.AddInventory(s4, re2, 6);
            db.AddInventory(s4, mgs, 3);
            db.AddInventory(s4, ff7, 2);
            db.AddInventory(s4, nights, 4);
            db.AddInventory(s4, n64cont, 4);
            db.AddInventory(s4, n64expansion, 1);
            db.AddInventory(s4, rumblepak, 5);
            db.AddInventory(s4, n64mem, 3);
            db.AddInventory(s4, psds, 4);
            db.AddInventory(s4, psmem, 6);
            db.AddInventory(s4, saturncont, 5);
            db.AddInventory(s4, saturngun, 2);






        }
    }
}





































