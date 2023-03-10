namespace ETrade.API.Models
{
    public class Customer
    {
        public Customer()
        {
            Carts = new List<Cart>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; } 

        public string? Payment { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public List<Cart>? Carts { get; set; } 

    }
}
