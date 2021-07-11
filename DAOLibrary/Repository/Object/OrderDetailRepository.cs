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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderId) => OrderDetailDAO.Instance.GetOrderDetailsByOrderId(orderId);
        public void AddOrderDetailtoOrder(OrderDetail detail) => OrderDetailDAO.Instance.AddOrderDetailtoOrder(detail);
    }
}
