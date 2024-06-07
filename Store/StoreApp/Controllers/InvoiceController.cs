using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class InvoiceController : Controller
    {
        private IRepositoryManager _manager;

        public InvoiceController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult ViewInvoice(int orderId)
        {
            var invoice = _manager.Invoice.GetInvoiceByOrderId(orderId);

            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }
    }
}