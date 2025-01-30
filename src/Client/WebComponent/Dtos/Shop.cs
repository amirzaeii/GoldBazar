namespace WebComponent.Dtos
{
    public record Shop (int Id, string Name,string Address, string City, string ContactNo, string OwnerName)
    {
        public string ImageUrl { get; set; } = "_content/WebComponent/assets/Image/avita.svg";
    }
}
//public string ImageUrl { get; set; } = "_content/WebComponent/assets/Image/avita.svg";
//public int ReviewCount { get; set; } = 57;