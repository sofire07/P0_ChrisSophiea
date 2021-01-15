using System.Collections.Generic;

namespace P0_ChrisSophiea

{
    public interface DAOMethods
    {
        public ICollection<Store> GetAllStores();
        public void PrintAllStores();
        public Store GetStoreById(int id);
        public Store GetStoreByAddress(string address);
        public Store GetStoreByPhone(string phone);
        public ICollection<Purchase> GetPurchasesByStore(Store store);

        public void AddStore(Store s);
        public void DeleteStore(Store s);
        public void UpdateStore(Store s, string address, string phone);
        public ICollection<Purchase> GetAllPurchases();
        public ICollection<Purchase> GetPurchasesByCustomer(Customer c);
        public ICollection<Purchase> GetPurchasesByItem(Item item);
        public void AddPurchase(Purchase p);
        public void DeletePurchase(Purchase p);

        public List<Item> GetAllItems();
        public Item GetItemById(int id);
        public List<Item> GetItemsByName(string itemName);
        public List<Item> GetItemsByType(string type);

        public void AddItem(Item i);
        public void DeleteItem(Item i);
        public void UpdateItem(Item i, string newItemName, string newDescription, double newItemPrice);

        public ICollection<Customer> GetAllCustomers();
        public ICollection<Customer> GetCustomersByName(string fname, string lname);
        public ICollection<Customer> GetCustomersByEmail(string email);

        public Customer AddCustomer(Customer c);
        public void DeleteCustomer(Customer c);
        public void UpdateCustomer(Customer c, string newfname, string newlname, string newemail);

        public ICollection<Inventory> GetInventory(Store store);
        public ICollection<Inventory> GetInventoryByType(Store store, string itemType);
        public Inventory GetInventoryById(int Id);

        public void PrintAllInventory(Store store);
        public void PrintInventoryByType(Store store, string type);

        public void AddInventory(Store store, Item item, int amount);
        public void ReduceInventory(Inventory i, int amount);

    }
}