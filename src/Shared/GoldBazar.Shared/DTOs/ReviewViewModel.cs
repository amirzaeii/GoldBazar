namespace GoldBazar.Shared.DTOs
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int GoldSmithId { get; set; }
        public int UserId { get; set; }
        public int Rate { get; set; } // 1-5 stars
        public DateTime ReviewDate { get; set; }
    }
}
