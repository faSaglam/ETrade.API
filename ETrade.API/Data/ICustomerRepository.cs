using ETrade.API.Models;

namespace ETrade.API.Data
{
    public interface ICustomerRepository
    {
        Task<Customer> Register(Customer customer, string password);
        Task<Customer> Login(string UserName, string password);
        Task<bool> CustomerExist(string userName, string email);

      

    

    }
}
