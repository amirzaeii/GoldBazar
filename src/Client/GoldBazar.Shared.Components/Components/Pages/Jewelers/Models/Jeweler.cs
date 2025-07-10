namespace GoldBazar.Client.Web.Components.Pages.Jewelers.Models

{
    public class Jeweler
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? LogoUrl { get; set; }

        public float? Rating { get; set; }
        public int ReviewsCount { get; set; }
    }
}