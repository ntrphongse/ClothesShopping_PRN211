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
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetProductList() => ProductDAO.Instance.GetProductListAsync();
    }
}
