namespace GoldBazar.Shared.DTOs
{  //Subplan could be enum
    public record ShopDTO (int Id,
      string Name,
      string Address,
      string CityName,
      int CityId, 
      string ContactNo,
      string OwnerName,
      bool Status,
      string CurrentSubscriptionPlanPrice,
      string CurrentSubscriptionPlanName,
      string LogoUrl,
      string BannerUrl,
      string Description,
      DateTime JoinDate,
      DateTime SubscriptionExpiredDate,
      int OrdersDelivered, 
      int UpComingOrders )
    {
        public string ImageUrl { get; init; } = "_content/WebComponent/assets/Image/avita.svg";
        public string ContactNo { get; init; } = "0770 000 000";
        public string OwnerName { get; init; } = "John Doe";
        public bool Status { get; init; } = true;
        public string SubPlan { get; init; } = "VIP";
        public DateTime Join { get; init; }= DateTime.UtcNow;
    }
}
