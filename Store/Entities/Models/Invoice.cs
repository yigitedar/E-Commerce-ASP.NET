using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; } // Foreign Key
        public Order Order { get; set; } // Navigation property
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string ProductDetails { get; set; } // JSON or other format to store product details
    }
}
