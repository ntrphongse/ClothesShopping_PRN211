using ClothesShoppingWebApp.Models;
using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers.Guest
{
    public class GuestController : Controller
    {
        IProductRepository productRepository = null;
        ICategoryRepository categoryRepository = null;
        public GuestController()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }
        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            IEnumerable<Product> listProduct = productRepository.GetProductList();
            IEnumerable<Category> listCategory = categoryRepository.GetCategoryList();
            GuestViewModel models = new GuestViewModel();
            models.Products = listProduct;
            models.Categories = listCategory;
            if (TempData["CheckoutMessage"] != null)
            {
                ViewBag.Message = TempData["CheckoutMessage"];
                TempData.Remove("CheckoutMessage");
            }
            return View(models);
        }

        //GET - FILTER
        [Authorize(Policy = "UserPolicy")]
        public IActionResult Filter(string CategoryName)
        {
            if(CategoryName == null)
            {
                return NotFound();
            }
            var category = categoryRepository.GetCategoryByName(CategoryName);
            if(category == null)
            {
                return NotFound();
            }
            IEnumerable<Product> listProduct = productRepository.GetProductList()
                .Where(p => p.CategoryId == category.CategoryId).ToList();
            IEnumerable<Category> listCategory = categoryRepository.GetCategoryList();
            GuestViewModel models = new GuestViewModel();
            models.Products = listProduct;
            models.Categories = listCategory;
            HttpContext.Session.SetString("SelectedCat", CategoryName);
            return View("Index",models);
        }

        //POST - SEARCH
        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string ProductName)
        {
            GuestViewModel models = new GuestViewModel();
            IEnumerable<Category> listCategory = categoryRepository.GetCategoryList();
            IEnumerable<Product> listProduct = null;
            var SelectedCat = HttpContext.Session.GetString("SelectedCat");
            if (SelectedCat == null)
            {
                listProduct = productRepository.GetProductList()
                     .Where(p => p.ProductName.ToLower().Contains(ProductName.ToLower())).ToList();
            }
            else
            {
                int CategoryId = categoryRepository.GetCategoryByName(SelectedCat).CategoryId;
                listProduct = productRepository.GetProductList()
                    .Where(p => p.CategoryId == CategoryId)
                    .Where(p => p.ProductName.ToLower().Contains(ProductName.ToLower())).ToList();
            }        
            models.Products = listProduct;
            models.Categories = listCategory;
            ViewBag.SearchValue = ProductName;
            return View("Index",models);
        }
    }
}


