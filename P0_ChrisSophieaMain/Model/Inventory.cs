using System;
using System.ComponentModel.DataAnnotations;

namespace P0_ChrisSophiea
{
    public class Inventory
    {

        [Key]
        public int InventoryId { get; set; }
        public int Store1Id { get; set; }
        public Store Store1 { get; set; }
        public int Item1Id { get; set; }
        public Item Item1 { get; set; }
        public int InventoryAmount { get; set; } = 0;

        public override string ToString()
        {
            return $"\n{InventoryId}. {Item1} | In Stock: {InventoryAmount}";
        }


    }
}