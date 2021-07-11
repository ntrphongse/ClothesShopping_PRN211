using ClothesShoppingWebApp.Models;
using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers
{
    public class CheckoutController : Controller
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        public CheckoutController()
        {
            IOrderRepository orderRepository = new OrderRepository();
            IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Checkout(float TotalPrice, int NumOfItems, int UserId, string Address)
        {
            bool quantitycheck = false; //false = OK, true = Exception
            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            List<CartItem> list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            foreach (var item in list)
            {
                var pro = productRepository.GetProductById(item.ProductId);
                if (item.Quantity > pro.Quantity) 
                { 
                    quantitycheck = true;
                    break;
                }
            }
            if (quantitycheck == true)
            {
                return RedirectToAction("Checkout", "Cart");
            }
            else
            {
                Order order = new Order()
                {
                    NumberOfItem = NumOfItems,
                    Address = Address,
                    TotalPrice = TotalPrice,
                    CustomerId = UserId,
                    CreatedDate = DateTime.Now
                };
                int orderId = orderRepository.CreateOrder(order);
                OrderDetail orderDetail;
                foreach (var item in list)
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        ItemPrice = item.Price,
                        Quantity = item.Quantity
                    };
                    orderDetailRepository.AddOrderDetailtoOrder(orderDetail);
                }
                list.Clear();
                HttpContext.Session.SetComplexData("CART", list);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
