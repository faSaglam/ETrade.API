using ETrade.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ETrade.API.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;
        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
        
           _context.Add(entity);
        }

        //public void Update<T>(T entity) where T : class
        //{


        //    _context.Set<T>().Update(entity);


        //}
        public async Task Put<T>(T entity) where T : class
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();


        }
       


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories.Include(p=>p.Products).ToList();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Include(p => p.Products).FirstOrDefault(c => c.Id == id);
            return category;
        }
  
        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id);
            return product;
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _context.Products.Where(p=>p.CategoryId==categoryId).ToList();
            return products;
        }
        public Seller GetSellerById(int sellerId)
        {
            var seller = _context.Sellers.Include(p=>p.Products).FirstOrDefault(c => c.Id == sellerId);
            return seller;
        }


        public bool SaveAll() => _context.SaveChanges() > 0;

        public Task Update<T>(T entity) where T : class
        {
           _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
        

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Cart> GetCartItemAsync(int id)
        {
            return await _context.Carts.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public Task Patch<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
        //Cart of Customer

        public Customer GetCustomerById(int customerId)
        {
            var customer = _context.Customers.Include(c=>c.Carts).FirstOrDefault(c=>c.Id==customerId);
            return customer;
        }
        public List<Cart> GetCartByCustomerId(int customerId) 
        {
            var cart = _context.Carts.Where(c=>c.customerId == customerId).ToList();
            return cart;

        }
       

        public Cart GetCartItem(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            return cart;
        }
        public List<Cart> GetCart()
        {
            var cart = _context.Carts.ToList();
            return cart;

        }

        public List<Product> GetProductsBySellerId(int sellerId)
        {
           var product = _context.Products.Where(p => p.SellerId == sellerId).ToList();
            return product;
        }

        public List<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }
    }
}
