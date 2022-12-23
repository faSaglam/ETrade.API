namespace ETrade.API.Models
{
    public class Seller
    {
        public Seller()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }  

        public string? Adress { get; set; } 

        public string ?Payment { get; set; } 

        public byte[] ?PasswordHash { get; set; }
        public byte[] ?PasswordSalt { get; set; }

        public List<Product>? Products { get; set; }
    }
}
