using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

namespace StoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public OrderController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }

        //[Authorize]
        //public ViewResult Checkout() => View(new Order());


        [Authorize]
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = new Order
            {
                UserId = userId
            };
            return View(order);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm] Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("","Sorry, your cart is empty.");
            }

            if(ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _manager.OrderService.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Complete", new {OrderId = order.OrderId});
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public IActionResult MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _manager.OrderService.Orders.Where(o => o.UserId == userId);
            return View(orders);
        }
    }
}