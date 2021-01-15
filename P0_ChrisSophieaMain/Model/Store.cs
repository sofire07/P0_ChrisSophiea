using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P0_ChrisSophiea
{
    public class Store
    {
        //private Guid storeId = Guid.NewGuid();
        //
        [Key]
        public int StoreId { get; set; }


        public string StoreAddress { get; set; }

        public string PhoneNumber { get; set; }

        //[InverseProperty("InventoryId")]
        public ICollection<Inventory> Inventories { get; set; }

        public ICollection<Purchase> Purchases { get; set; }


        public override string ToString()
        {
            return $"{StoreId}. Store Address: {StoreAddress} | Phone Number: {PhoneNumber}";
        }


    }
}