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
        Product GetProductById(int id);
        IEnumerable<Product> GetProductList();
        void CreatePostProduct(Product product);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
    }
}
