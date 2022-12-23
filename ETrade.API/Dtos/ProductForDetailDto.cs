namespace ETrade.API.Dtos
{
    public class ProductForDetailDto
    {
        public string ?ProductName { get; set; }
        public string ?SellerName { get; set; }

        public decimal UnitPrice { get; set; }

        public string? Defination { get; set; }
        public string ?PhotoUrl { get; set; }
    }
}
