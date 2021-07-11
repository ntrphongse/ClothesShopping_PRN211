using DAOLibrary.DataAccess;
using DAOLibrary.Repository.Interface;
using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository.Object
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrderList();


        public IEnumerable<Order> GetOrderByCustomerId(int id) => OrderDAO.Instance.GetOrdersByCustomerId(id);

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        int IOrderRepository.CreateOrder(Order order) => OrderDAO.Instance.CreateOrder(order);
    }
}
