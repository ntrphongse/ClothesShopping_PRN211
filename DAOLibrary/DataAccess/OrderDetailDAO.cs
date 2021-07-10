using DTOLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------------------------

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int id)
        {
            var orders = new List<OrderDetail>();
            try
            {
                using var context = new lPVNgP26wKContext();
                orders = context.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }
    }
}
