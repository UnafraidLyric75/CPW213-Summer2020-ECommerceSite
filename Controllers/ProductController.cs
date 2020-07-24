using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a view that list all products
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // Get all product from database
            List<Product> products = _context.Products.ToList();
            // or
            //List<Product> products =
            //    (from p in _context.Products
            //     select p).ToList();

            // Send list of products to view to be display
            return View(products);
        }
    }
}
