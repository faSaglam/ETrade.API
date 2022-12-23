using System.Runtime.CompilerServices;

namespace ETrade.API.Models
{
    public class Cart
    {
        public int ?Id { get; set; }
        public int ?quantity { get; set; }
        public decimal ?price { get; set; }

        public bool ?isOnWay { get; set; }

        public bool? isDelivered { get; set; }

        public string productName { get; set; }

        public string photoUrl { get; set; }

        public int customerId { get; set; }

        public int productId { get; set; }
    }
}
