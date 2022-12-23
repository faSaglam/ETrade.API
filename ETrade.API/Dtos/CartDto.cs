namespace ETrade.API.Dtos
{
    public class CartDto
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

        public string productName { get; set; }
        public string photoUrl { get; set; }
    }
}
