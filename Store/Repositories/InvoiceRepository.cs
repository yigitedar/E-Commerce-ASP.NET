using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {

        public InvoiceRepository(RepositoryContext context) : base(context)
        {
           
        }

        public void Add(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }

        public Invoice GetInvoiceByOrderId(int orderId)
        {
            return _context.Invoices
           .Include(i => i.Order)
           .FirstOrDefault(i => i.OrderId == orderId);
        }

        // Diğer gerekli metotları implemente edin
    }
}
