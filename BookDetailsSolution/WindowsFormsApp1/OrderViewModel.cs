

using System;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
 
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPay { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
