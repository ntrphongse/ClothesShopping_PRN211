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

        public void CreatePostProduct(Product product) => ProductDAO.Instance.CreatePost(product);


        public void DeleteProduct(int productId) => ProductDAO.Instance.Remove(productId);

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);
        
        public IEnumerable<Product> GetProductList() => ProductDAO.Instance.GetProductList();

        public void UpdateProduct(Product product) => ProductDAO.Instance.Update(product);

        
    }
}
