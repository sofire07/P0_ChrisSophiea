using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P0_ChrisSophiea
{
    public class DAOMethodsImpl : DAOMethods
    {
        DAOUtility db = new DAOUtility();

        public DAOMethodsImpl() { }
        public DAOMethodsImpl(DAOUtility context)
        {
            this.db = context;
        }

        /// <summary>
        /// Adds a customer to the database and returns the Customer object.
        /// </summary>
        /// <param name="c">Customer c</param>
        /// <returns>Returns the customer that was created and added to the database.</returns>
        public Customer AddCustomer(Customer c)
        {
            Customer customer = db.customers.Where(x => x.EmailAddress == c.EmailAddress).FirstOrDefault();
            if (customer == null)
            {
                db.customers.Add(c);

            }
            else
            {
                Console.WriteLine("Adding customer failed. Customer already exists in DB.");
            }
            db.SaveChanges();
            return db.customers.Where(x => x.EmailAddress == c.EmailAddress).First();
        }

        /// <summary>
        /// Deletes the given Customer c from the database.
        /// </summary>
        /// <param name="c">Customer c</param>
        public void DeleteCustomer(Customer c)
        {

            Customer customer = db.customers.Where(x => x.CustomerId == c.CustomerId).FirstOrDefault();
            if (customer != null)
            {
                db.customers.Remove(c);
            }
            else
            {
                Console.WriteLine("Removing customer failed. Customer doesn't exist in the DB.");
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>Returns ICollection of all customers in the database.</returns>
        public ICollection<Customer> GetAllCustomers()
        {
            // db.SaveChanges();
            return db.customers.ToList();
        }

        /// <summary>
        /// Prints all the customers in the database.
        /// </summary>
        public void PrintAllCustomers()
        {

            foreach (Customer c in db.customers.ToList())
            {
                Console.WriteLine(c);
            }
            // db.SaveChanges();
        }

        /// <summary>
        /// Returns ICollection of customers that have an email that matches the email address input parameter.
        /// </summary>
        /// <param name="email">string input parameter email</param>
        /// <returns>Returns ICollection of Customers</returns>
        public ICollection<Customer> GetCustomersByEmail(string email)
        {
            ICollection<Customer> customers = new List<Customer>();
            foreach (Customer c in db.customers.ToList())
            {
                if (c.EmailAddress == email)
                {
                    customers.Add(c);
                }
            }
            return customers;
        }

        /// <summary>
        /// Searches the database for Customers with First Name and Last Name input parameters.
        /// </summary>
        /// <param name="fname">string Fname. Customer's first name</param>
        /// <param name="lname">string Lname. Customer's last name</param>
        /// <returns>Returns ICollection of Customers that match First and Last Name given as input.</returns>
        public ICollection<Customer> GetCustomersByName(string fname, string lname)
        {
            ICollection<Customer> customers = new List<Customer>();
            foreach (Customer c in db.customers.ToList())
            {
                if (c.Fname == fname && c.Lname == lname)
                {
                    customers.Add(c);
                }
            }
            // db.SaveChanges();
            return customers;
        }

        /// <summary>
        /// Updates Customer in the database given the original Customer and new Fname, new Lname and new Email.
        /// </summary>
        /// <param name="c">Customer c - original customer</param>
        /// <param name="newfname">string newfname - new first name</param>
        /// <param name="newlname">string newlname - new last name</param>
        /// <param name="newemail">string newemail - new email</param>
        public void UpdateCustomer(Customer c, string newfname, string newlname, string newemail)
        {
            Customer customer = db.customers.Where(x => x.CustomerId == c.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                Console.WriteLine("Could not update customer. It doesn't exist in the DB.");
            }
            else
            {
                customer.Fname = newfname;
                customer.Lname = newlname;
                customer.EmailAddress = newemail;
                db.customers.Update(customer);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Add inventory to a store. Takes parameters Store, Item, and amount.
        /// If inventory already exists, it increases the inventory amount by the given amount.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void AddInventory(Store store, Item item, int amount)
        {
            Inventory i = new Inventory();
            i = db.inventories.Include(a => a.Store1).Include(a => a.Item1).Where(x => x.Store1.StoreAddress == store.StoreAddress && x.Item1.ItemName == item.ItemName).FirstOrDefault();
            if (i == null)
            {
                i = new Inventory();
                i.Store1 = store;
                i.Item1 = item;
                i.InventoryAmount += amount;
                // db.stores.Attach(store);
                // db.items.Attach(item);
                db.inventories.Add(i);

            }
            else
            {
                i.InventoryAmount += amount;
                // db.stores.Attach(i.Store1);
                // db.items.Attach(i.Item1);
                db.inventories.Update(i);

            }

            db.SaveChanges();
        }

        /// <summary>
        /// Gets ICollection of Inventory objects from a Store given as an input parameter.
        /// </summary>
        /// <param name="store">Store s</param>
        /// <returns>ICollection of Inventories</returns>
        public ICollection<Inventory> GetInventory(Store store)
        {

            ICollection<Inventory> allInventory = new List<Inventory>();
            foreach (Inventory i in db.inventories.ToList())
            {
                if (i.Store1.StoreId == store.StoreId)
                {
                    allInventory.Add(i);
                    //db.Entry(i.Store1).State = 0;
                }
            }
            //db.SaveChanges();
            return allInventory;
        }

        /// <summary>
        /// Gets ICollection of Inventories of a specific item type from a Store.
        /// </summary>
        /// <param name="store">Store store</param>
        /// <param name="itemType">string itemType - category of item</param>
        /// <returns>ICollection of Inventories</returns>
        public ICollection<Inventory> GetInventoryByType(Store store, string itemType)
        {

            ICollection<Inventory> inventories = new List<Inventory>();
            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId && i.Item1.ItemType == itemType)
                {
                    inventories.Add(i);
                    // db.Entry(i.Store1).State = 0;
                    // db.Entry(i.Item1).State = 0;
                }
            }
            // db.SaveChanges();
            return inventories;
        }

        /// <summary>
        /// Get Inventory based on it's Inventory ID.
        /// </summary>
        /// <param name="Id">int id - Inventory Id</param>
        /// <returns>Return Inventory object</returns>
        public Inventory GetInventoryById(int Id)
        {

            Inventory inventory = new Inventory();
            foreach (Inventory i in db.inventories.Include(a => a.Item1).Include(a => a.Store1).ToList())
            {
                if (i.InventoryId == Id)
                {
                    inventory = i;
                }
            }
            //db.SaveChanges();
            return inventory;
        }

        /// <summary>
        /// Prints out all the inventories from a given store.
        /// </summary>
        /// <param name="store">Store store</param>
        public void PrintAllInventory(Store store)
        {

            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId)
                {
                    Console.WriteLine(i);
                }
            }
            // db.SaveChanges();

        }

        /// <summary>
        /// Prints out all inventories from a store of a specific item type.
        /// </summary>
        /// <param name="store">Store s</param>
        /// <param name="type">string type - item type</param>
        public void PrintInventoryByType(Store store, string type)
        {

            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId && i.Item1.ItemType == type)
                {
                    Console.WriteLine(i);

                }
            }
            //db.SaveChanges();

        }

        /// <summary>
        /// Reduces the quantity of inventory given the inventory and amount.
        /// </summary>
        /// <param name="i">Inventory i</param>
        /// <param name="amount">int amount - amount to reduce available inventory</param>
        public void ReduceInventory(Inventory i, int amount)
        {
            Inventory inventory = new Inventory();
            inventory = db.inventories.Where(x => x.InventoryId == i.InventoryId).FirstOrDefault();
            if (i.InventoryAmount - amount >= 0)
            {
                inventory.InventoryAmount -= amount;
                db.inventories.Update(inventory);


            }
            else
            {
                Console.WriteLine("Could not reduce inventory. Not that much instock.");
            }
            db.SaveChanges();

        }

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="i">Item i</param>
        public void AddItem(Item i)
        {
            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item == null)
            {
                db.items.Add(i);

            }
            else
            {
                Console.WriteLine("Adding item failed. Item already exists in DB.");
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="i">Item i</param>
        public void DeleteItem(Item i)
        {
            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item != null)
            {
                db.items.Remove(i);
            }
            else
            {
                Console.WriteLine("Removing item failed. Item doesn't exist in DB.");
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Get an Item based on the given Item ID.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Item</returns>
        public Item GetItemById(int id)
        {

            return db.items.Where(x => x.ItemId == id).FirstOrDefault();
        }

        /// <summary>
        /// Returns a List of all Items in the database.
        /// </summary>
        /// <returns>List of all items in database</returns>
        public List<Item> GetAllItems()
        {
            //db.SaveChanges();
            return db.items.ToList();
        }

        /// <summary>
        /// Returns a list of all items of a specific type.
        /// </summary>
        /// <param name="type">string type - item type</param>
        /// <returns>List of Items</returns>
        public List<Item> GetItemsByType(string type)
        {
            //db.SaveChanges();
            return db.items.Where(x => x.ItemType == type).ToList();
        }

        /// <summary>
        /// Returns a list of items with a specific name.
        /// </summary>
        /// <param name="itemName">string itemName - name of the item</param>
        /// <returns>List of Items</returns>
        public List<Item> GetItemsByName(string itemName)
        {

            List<Item> itemsListByName = new List<Item>();
            foreach (Item i in db.items)
            {
                if (i.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    itemsListByName.Add(i);
                }
            }
            //db.SaveChanges();
            return itemsListByName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="newItemName"></param>
        /// <param name="newItemDescrition"></param>
        /// <param name="newItemPrice"></param>
        public void UpdateItem(Item i, string newItemName, string newItemDescrition, double newItemPrice)
        {

            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("Could not update item. It doesn't exist in the DB.");
            }
            else
            {
                item.ItemName = newItemName;
                item.ItemPrice = newItemPrice;
                db.items.Update(item);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Adds a purchase to the database.
        /// </summary>
        /// <param name="p">Purchase p</param>
        public void AddPurchase(Purchase p)
        {
            Purchase purchase = new Purchase();
            purchase = db.purchases.Include(a => a.Item1).Include(a => a.Store1).Where(x => x.PurchaseId == p.PurchaseId).FirstOrDefault();
            if (purchase == null)
            {
                if (p.TotalPrice != 0)
                {
                    db.purchases.Add(p);

                }
                else
                {
                    Console.WriteLine("You didn't select any items to purchase.");
                }
            }
            else
            {
                Console.WriteLine("This purchase already went through.");
            }
            db.SaveChanges();


        }

        /// <summary>
        /// Deletes a given purchase from the Database.
        /// </summary>
        /// <param name="p">Purchase p</param>
        public void DeletePurchase(Purchase p)
        {

            Purchase purchase = new Purchase();
            purchase = db.purchases.Where(x => x.PurchaseId == p.PurchaseId).FirstOrDefault();
            if (purchase != null)
            {
                db.purchases.Remove(purchase);
            }
            else
            {
                Console.WriteLine("Can't remove purchase. It doesn't exist in the DB.");
            }

        }

        /// <summary>
        /// Get all purchases from the database.
        /// </summary>
        /// <returns>ICollection of purchases</returns>
        public ICollection<Purchase> GetAllPurchases()
        {
            return db.purchases.ToList();
        }

        /// <summary>
        /// Get all purchases made by a specific customer.
        /// </summary>
        /// <param name="c">Customer c</param>
        /// <returns>ICollection of Purchases</returns>
        public ICollection<Purchase> GetPurchasesByCustomer(Customer c)
        {
            ICollection<Purchase> purchaseListByCustomer = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                if (purchase.Customer1.CustomerId == c.CustomerId)
                {
                    purchaseListByCustomer.Add(purchase);
                }
            }
            return purchaseListByCustomer;
        }

        /// <summary>
        /// Get all purchases made at a specific store.
        /// </summary>
        /// <param name="store">Store store</param>
        /// <returns>ICollection of Purchases</returns>
        public ICollection<Purchase> GetPurchasesByStore(Store store)
        {
            ICollection<Purchase> purchaseListByStore = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                if (purchase.Store1.StoreId == store.StoreId)
                {
                    purchaseListByStore.Add(purchase);
                }
            }
            return purchaseListByStore;
        }

        /// <summary>
        /// Get all purchases with a specific item in it.
        /// </summary>
        /// <param name="item">Item item</param>
        /// <returns>ICollection of Purchases</returns>
        public ICollection<Purchase> GetPurchasesByItem(Item item)
        {
            ICollection<Purchase> purchaseListByItem = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                foreach (Item i in purchase.Item1)
                {
                    if (i.ItemId == item.ItemId)
                    {
                        purchaseListByItem.Add(purchase);
                    }
                }
            }
            return purchaseListByItem;
        }


        /// <summary>
        /// Add a Store to the database.
        /// </summary>
        /// <param name="s">Store s</param>
        public void AddStore(Store s)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store == null)
            {
                db.stores.Add(s);
                //
            }
            else
            {
                Console.WriteLine("Adding store failed. Store already exists in DB.");
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Delete a store from the database.
        /// </summary>
        /// <param name="s">Store s</param>
        public void DeleteStore(Store s)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store != null)
            {
                db.stores.Remove(s);
                // db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Store deletion failed. Store doesn't exist in DB.");
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Get a list of all the stores in the database.
        /// </summary>
        /// <returns>ICollection of Stores</returns>
        public ICollection<Store> GetAllStores()
        {
            ICollection<Store> stores = new List<Store>();
            foreach (Store s in db.stores.ToList())
            {
                stores.Add(s);
            }
            return stores;
        }

        /// <summary>
        /// Prints all the stores in the database.
        /// </summary>
        public void PrintAllStores()
        {
            foreach (Store s in db.stores.ToList())
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Gets a store with the given address.
        /// </summary>
        /// <param name="s">string s - store address</param>
        /// <returns>Store</returns>
        public Store GetStoreByAddress(string s)
        {
            Store store = new Store();
            foreach (Store s1 in db.stores.ToList())
            {
                if (s1.StoreAddress == s)
                {
                    store = s1;
                }
            }
            return store;
        }

        /// <summary>
        /// Gets the store with the given store ID.
        /// </summary>
        /// <param name="id">int id - store ID</param>
        /// <returns>Store</returns>
        public Store GetStoreById(int id)
        {
            Store store = new Store();
            foreach (Store s in db.stores.ToList())
            {
                if (s.StoreId == id)
                {
                    store = s;
                }
            }
            return store;
        }

        /// <summary>
        /// Gets a store based on the given phone number.
        /// </summary>
        /// <param name="p">string p - phone number</param>
        /// <returns>Store</returns>
        public Store GetStoreByPhone(string p)
        {
            Store store = db.stores.Where(x => x.PhoneNumber == p).FirstOrDefault();
            return store;
        }

        /// <summary>
        /// Update a store in the database given the store, a new address and new phone #.
        /// </summary>
        /// <param name="s">Store s</param>
        /// <param name="address">string address - store address</param>
        /// <param name="phone">string phone - store phone #</param>
        public void UpdateStore(Store s, string address, string phone)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store == null)
            {
                Console.WriteLine("Could not update store. It doesn't exist in the DB.");
            }
            else
            {
                store.StoreAddress = address;
                store.PhoneNumber = phone;
                db.stores.Update(store);
                //
            }
            db.SaveChanges();

        }


    }
}