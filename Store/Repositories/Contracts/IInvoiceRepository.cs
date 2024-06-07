using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        Invoice GetInvoiceByOrderId(int orderId);
        // Diğer gerekli metotları tanımlayın
    }
}
