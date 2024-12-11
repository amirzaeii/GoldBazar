
namespace Catalog.Infrastructure
{
   public record ShopDto
    {
        public ShopDto()
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public float Rating { get; set; }
    }
}
