using ETrade.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security;

namespace ETrade.API.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CustomerExist(string userName, string email)
        {
      
            if (await _context.Customers.AnyAsync(c => c.UserName == userName) || await _context.Customers.AnyAsync(c => c.Email == email))
            { return true; };
            return false;
        }

 
    
        public async Task<Customer> Login(string UserName, string password)
        {
     
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserName == UserName);
            if(customer == null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password , customer.PasswordHash , customer.PasswordSalt))
            {
                return null;
            }
            return customer;
        }

        public async Task<Customer> Register(Customer customer, string password)
        {
        

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            customer.PasswordHash = passwordHash; 
            customer.PasswordSalt = passwordSalt;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        //passwordhash - salt
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
    }
}
