using ETrade.API.Models;
using System.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ETrade.API.Data
{
    public class SellerRepository : ISellerRepository
    {
        private DataContext _context;
        public SellerRepository(DataContext context)
        {
            _context = context;
        }

        //passwordhash - passwordsalt 
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            { 
              passwordSalt = hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
          

        }
        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        public async Task<Seller> Login(string UserName, string password)
        {
            var user = await _context.Sellers.FirstOrDefaultAsync(x => x.UserName == UserName);
            if(user == null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        //UserExist ( for seller)
        public async Task<bool> SellerExist(string userName , string email)
        {
            if (await _context.Sellers.AnyAsync(x => x.UserName == userName) || await _context.Sellers.AnyAsync(e=>e.Email == email))
            { return true; }
            return false;
        }


        public async Task<Seller> Register(Seller seller, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            seller.PasswordHash = passwordHash;
            seller.PasswordSalt = passwordSalt;
            await _context.Sellers.AddAsync(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        //seller'ın productlarını listelemek için 

        public Seller GetSellerById(int id)
        {
            var seller = _context.Sellers.Include(p=>p.Products).FirstOrDefault(x => x.Id == id);
            return seller;
        }

        public List<Product> GetProductsBySellerId(int sellerId)
        {
            var products = _context.Products.Where(x => x.SellerId == sellerId).ToList();
            return products;
        }
    }
}
