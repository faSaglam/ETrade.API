using ETrade.API.Models;

namespace ETrade.API.Data

{
    public interface ISellerRepository
    {
        Task<Seller> Register(Seller seller, string password);
        Task<Seller> Login(string UserName , string password);
        Task<bool> SellerExist(string userName , string email);

        Seller GetSellerById(int id);
        List<Product> GetProductsBySellerId(int sellerId);
        
        //Product GetProduct(int id);
    }
}
