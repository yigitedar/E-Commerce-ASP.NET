using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class OrderManager : IOrderService
    {
        private readonly IRepositoryManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderManager(IRepositoryManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<Order> Orders => _manager.Order.Orders;

        public int NumberOfInProcess => _manager.Order.NumberOfInProcess;

        public void Complete(int id)
        {
            _manager.Order.Complete(id);
            _manager.Save();
        }

        public Order? GetOneOrder(int id)
        {
            return _manager.Order.GetOneOrder(id);
        }

        public void SaveOrder(Order order)
        {
            // Kullanýcý ID'sini ayarla
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            order.UserId = userId;

            _manager.Order.SaveOrder(order);
            CreateInvoice(order);
        }

        private void CreateInvoice(Order order)
        {
            var productDetails = JsonConvert.SerializeObject(order.Lines.Select(cl => new
            {
                ProductName = cl.Product.ProductName,
                Quantity = cl.Quantity,
                UnitPrice = cl.Product.Price,
                TotalPrice = cl.Quantity * cl.Product.Price
            }));

            var invoice = new Invoice
            {
                OrderId = order.OrderId,
                InvoiceDate = DateTime.Now,
                TotalAmount = order.Lines.Sum(cl => cl.Quantity * cl.Product.Price),
                ProductDetails = productDetails
            };

            _manager.Invoice.Add(invoice);
            _manager.Save();
        }
    }

}