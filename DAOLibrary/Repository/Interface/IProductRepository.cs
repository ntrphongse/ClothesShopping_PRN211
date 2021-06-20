using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductList();
    }
}
