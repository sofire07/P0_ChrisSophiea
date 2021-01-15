using System;
using P0_ChrisSophiea;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace P0_ChrisSophiea
{
    public class UnitTest1
    {
        [Fact]
        public void AddCustomerSavesCustomerToDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Customer c1 = new Customer();
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                c1.Fname = "Chris";
                c1.Lname = "Sophiea";
                c1.EmailAddress = "csophiea@gmail.com";
                c1 = repo.AddCustomer(c1);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                Customer c2 = new Customer();
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);
                c2.Fname = "Chris";
                c2.Lname = "Sophiea";
                c2.EmailAddress = "csophiea@gmail.com";
                c2 = repo.AddCustomer(c2);
                Assert.Equal(c1.CustomerId, c2.CustomerId);
            }


        }
        [Fact]
        public void DeleteCustomerDeletesCustomerFromDB()
        {

            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Customer c1 = new Customer();
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                c1.Fname = "Chris";
                c1.Lname = "Sophiea";
                c1.EmailAddress = "csophiea@gmail.com";
                c1 = repo.AddCustomer(c1);

                repo.DeleteCustomer(c1);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.customers.Where(x => x.EmailAddress == c1.EmailAddress).FirstOrDefault() == null);
            }

        }

        [Fact]
        public void UpdateCustomerUpdatesCustomerInfoInDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Customer c1 = new Customer();
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                c1.Fname = "Chris";
                c1.Lname = "Sophiea";
                c1.EmailAddress = "csophiea@gmail.com";
                c1 = repo.AddCustomer(c1);

                repo.UpdateCustomer(c1, "David", "Sophiea", "csophiea@gmail.com");
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.customers.Where(x => x.Fname == "David").FirstOrDefault() != null);
            }
        }

        [Fact]
        public void AddItemSavesItemToDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Item i1 = new Item()
            {
                ItemName = "PlayStation",
                ItemType = "Console",
                ItemDescription = "The first sony video-game console.",
                ItemPrice = 199.99
            };
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                repo.AddItem(i1);

            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.items.Where(x => x.ItemName == "PlayStation").FirstOrDefault() != null);
            }

        }

        [Fact]
        public void RemoveItemDeletesItemFromDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Item i1 = new Item()
            {
                ItemName = "PlayStation",
                ItemType = "Console",
                ItemDescription = "The first sony video-game console.",
                ItemPrice = 199.99
            };
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                repo.AddItem(i1);

                repo.DeleteItem(i1);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.items.Where(x => x.ItemName == "PlayStation").FirstOrDefault() == null);
            }

        }

        [Fact]
        public void AddStoreSavesStoreToDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Store s1 = new Store()
            {
                StoreAddress = "123 Fake St, Phoenix, AZ",
                PhoneNumber = "123-555-4567"
            };
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                repo.AddStore(s1);

            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.stores.Where(x => x.StoreAddress == "123 Fake St, Phoenix, AZ").FirstOrDefault() != null);
            }
        }

        [Fact]
        public void RemoveStoreDeletesStoreFromDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Store s1 = new Store()
            {
                StoreAddress = "123 Fake St, Phoenix, AZ",
                PhoneNumber = "123-555-4567"
            };
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);
                repo.AddStore(s1);

                repo.DeleteStore(s1);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.stores.Where(x => x.StoreAddress == "123 Fake St, Phoenix, AZ").FirstOrDefault() == null);
            }
        }

        [Fact]
        public void UpdateStoreUpdatesStoreInfoInDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            //act
            Store s1 = new Store()
            {
                StoreAddress = "123 Fake St, Phoenix, AZ",
                PhoneNumber = "123-555-4567"
            };
            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);

                repo.AddStore(s1);

                repo.UpdateStore(s1, "123 Fake St, Phoenix, AZ", "555-555-5555");
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.stores.Where(x => x.PhoneNumber == "555-555-5555").FirstOrDefault() != null);
            }

        }
        [Fact]
        public void AddInventorySavesInventoryToDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            Item i1 = new Item()
            {
                ItemName = "PlayStation",
                ItemType = "Console",
                ItemDescription = "The first sony video-game console.",
                ItemPrice = 199.99
            };

            Store s1 = new Store()
            {
                StoreAddress = "123 Fake St, Phoenix, AZ",
                PhoneNumber = "123-555-4567"
            };


            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);

                repo.AddInventory(s1, i1, 20);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.inventories.Where(x => x.Store1.StoreAddress == s1.StoreAddress && x.InventoryAmount == 20).FirstOrDefault() != null);
            }

        }

        [Fact]
        public void ReduceInventoryReducesInventoryInDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DAOUtility>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            Item i1 = new Item()
            {
                ItemName = "PlayStation",
                ItemType = "Console",
                ItemDescription = "The first sony video-game console.",
                ItemPrice = 199.99
            };

            Store s1 = new Store()
            {
                StoreAddress = "123 Fake St, Phoenix, AZ",
                PhoneNumber = "123-555-4567"
            };


            using (var context = new DAOUtility(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                DAOMethodsImpl repo = new DAOMethodsImpl(context);

                repo.AddInventory(s1, i1, 40);
                Inventory inventory = context.inventories.Where(x => x.Store1.StoreAddress == s1.StoreAddress && x.Item1.ItemId == i1.ItemId && x.InventoryAmount == 40).FirstOrDefault();

                repo.ReduceInventory(inventory, 30);
            }

            //assert
            using (var context1 = new DAOUtility(options))
            {
                DAOMethodsImpl repo = new DAOMethodsImpl(context1);

                Assert.True(context1.inventories.Where(x => x.Store1.StoreAddress == s1.StoreAddress && x.InventoryAmount == 10).FirstOrDefault() != null);
            }

        }


    }
}