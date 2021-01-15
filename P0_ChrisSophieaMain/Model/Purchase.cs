using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P0_ChrisSophiea
{
    public class Purchase
    {
        //private Guid purchaseId = Guid.NewGuid();
        //
        [Key]
        public int PurchaseId { get; set; }

        public int Customer1Id { get; set; }
        public Customer Customer1 { get; set; }
        public int Store1Id { get; set; }
        public Store Store1 { get; set; }
        public DateTime PurchaseDate { get; set; }
        public ICollection<Item> Item1 { get; set; }

        public double TotalPrice { get; set; }


        public override string ToString()
        {
            return $"Purchase ID: {PurchaseId} | {Customer1} | Store ID: {Store1} | Purchase Date: {PurchaseDate} | Total Price: {TotalPrice}";
        }

    }
}