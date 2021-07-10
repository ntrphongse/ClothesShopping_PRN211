using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers
{
    public class OrdersController : Controller
    {
        IOrderRepository orderRepository = null;
        IOrderDetailRepository orderDetailRepository = null;

        public OrdersController()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }

        public IActionResult Index()
        {
            var orderList = orderRepository.GetAllOrders();
            return View(orderList);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<OrderDetail> orderDetails;
            var order = orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            ViewData["OrderDetailList"] = orderDetails;
            return View(order);
        }
    }
}
