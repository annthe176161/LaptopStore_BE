using LaptopStore.Data.Contexts;
using LaptopStore.Data.Contracts;
using LaptopStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _context.Products.Update(product);

            }
        }

        public  IEnumerable<Product> GetAll()
        {
           return _context.Products.Where(p => !p.IsDeleted).ToList();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Where(p => !p.IsDeleted).ToListAsync();
        }

        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductID == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            return await _context.Products.
                Where(p => p.IsDiscounted && !p.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
        {
            return await _context.Products
                 .Where(p => p.BrandID == brandId && !p.IsDeleted).ToListAsync();
        }

        public IQueryable<Product> GetQuery()
        {
            return _context.Products.Where(p => !p.IsDeleted);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }
    }
}
