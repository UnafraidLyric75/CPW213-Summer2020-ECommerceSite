using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public static class ProductDB
    {
        /// <summary>
        /// Reuturns the total count of products
        /// </summary>
        /// <param name="_context">Database context</param>
        public async static Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await(from p in _context.Products
                  select p).CountAsync();
        }

        /// <summary>
        /// Get page worth of products
        /// </summary>
        /// <param name="_context">Database context</param>
        /// <param name="pageSize">num of products per page</param>
        /// <param name="pageNum">Page of products to return</param>
        /// <returns></returns>
        public async static Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {
            return await (from p in _context.Products
                          orderby p.Title ascending
                          select p)
                       .Skip(pageSize * (pageNum - 1))
                       .Take(pageSize)
                       .ToListAsync();
        }

        /// <summary>
        /// Adds products to database
        /// </summary>
        /// <param name="_context">Database context</param>
        /// <param name="p">is the product being added</param>
        /// <returns></returns>
        public async static Task<Product> AddProductAsync(ProductContext _context, Product p)
        {
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        } 
    }
}
