namespace WebComponent.Dtos
{
    public record Shop (int Id, string Name,string Address, string City, string ContactNo, string OwnerName)
    {
        public decimal Rate { get; init; } = 3.5M;
        public string ImageUrl { get; set; }
    }
}
//public string ImageUrl { get; set; } = "assets/Image/avita.svg";
//public int ReviewCount { get; set; } = 57;