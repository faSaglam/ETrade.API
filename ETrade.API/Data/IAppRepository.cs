using ETrade.API.Models;

namespace ETrade.API.Data
{
    public interface IAppRepository
    {
        void Add<T> (T entity) where T : class;
        //void Update<T> (T entity) where T : class;
        Task Update<T>(T entity) where T : class; //track den kurtulmak için async yapıldı
        void Delete<T> (T entity) where T : class;
        bool SaveAll();
        Task Patch<T>(T entity) where T : class;//stock işlemii için


        List<Category> GetCategories();
        List<Product> GetProductsByCategoryId(int categoryId);
        List<Product> GetProductsBySellerId(int sellerId);

        List<Product> GetAllProducts(); //Search işlemi için

       
        Category GetCategoryById(int id);
        Product GetProductById(int id);

        

        Task<Product> GetProductByIdAsync(int id);//track den kurtulmak için async yapıldı

        Seller GetSellerById(int sellerId);

        Cart GetCartItem(int id);
        List<Cart> GetCart();
        List<Cart> GetCartByCustomerId(int customerId);
        Customer GetCustomerById(int customerId);
  
        Task<Cart> GetCartItemAsync(int id);
       
    }
}
