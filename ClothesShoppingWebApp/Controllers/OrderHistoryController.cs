using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers
{
    public class OrderHistoryController : Controller
    {
        IOrderRepository orderRepository = null;
        IOrderDetailRepository orderDetailRepository = null;

        public OrderHistoryController()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }
        // GET: OrderHistoryController
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            string email = User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
            IUserRepository userRepository = new UserRepository();
            DTOLibrary.User user = userRepository.GetUser(email);
            var orderList = orderRepository.GetOrderByCustomerId(user.UserId);

            return View(orderList);
        }

        // GET: OrderHistoryController/Details/5
        [Authorize(Roles = "User")]
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
