namespace WebComponent.Dtos
{  //Subplan could be enum
    public record Shop (int Id, string Name,string Address, string City, string ContactNo, string OwnerName, bool Status, string SubPlan, DateTime Join, DateTime SubsValidUntil, int OrdersDelivered, int UpComingOrders )
    {
        public string ImageUrl { get; set; } = "_content/WebComponent/assets/Image/avita.svg";
        public string ContactNo { get; set; } = "0770 000 000";
        public string OwnerName { get; set; } = "John Doe";
        public bool Status { get; set; } = true;
        public string SubPlan { get; set; } = "VIP";
        public DateTime Join { get; set; }= DateTime.Now;
        public DateTime SubsValidUntil { get; set; } = DateTime.Now.AddDays(30);

    }
}
