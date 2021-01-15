using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P0_ChrisSophiea
{
    public class Item
    {
        //private Guid itemId = Guid.NewGuid();
        //
        [Key]
        public int ItemId { get; set; }

        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string ItemDescription { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public double ItemPrice { get; set; }

        public override string ToString()
        {
            return $"\nItem Name: {ItemName} | Item Type: {ItemType} | Item Description: {ItemDescription} | Price: {ItemPrice}";
        }
    }
}
