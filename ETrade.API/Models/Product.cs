namespace ETrade.API.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string ?ProductName { get; set; }
        public int Stock { get; set; }

        public string ?Defination { get; set; }

        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public int SellerId { get; set; }


        public string ?SellerName { get;set; }

        public string ?PhotoUrl { get; set; }

        //public string? CategoryName { get; set; }

        //photo bir listeteden alınabilir ... , sellername gereksiz olabilir...
    }
}
