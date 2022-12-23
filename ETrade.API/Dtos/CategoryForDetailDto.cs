using ETrade.API.Models;

namespace ETrade.API.Dtos
{
    public class CategoryForDetailDto
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
    }
}
