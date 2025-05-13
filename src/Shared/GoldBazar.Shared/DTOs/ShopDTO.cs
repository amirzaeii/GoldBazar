namespace GoldBazar.Shared.DTOs;
//Subplan could be enum
public record ShopDTO 
{
  public ShopDTO()
  {
    
  }
  public ShopDTO(int id,
   string name, 
   string address, 
   string cityName, 
   int cityId, 
   string contactNo,
   string ownerName,
   bool status, 
   string currentSubscriptionPlanPrice, 
   string currentSubscriptionPlanName, 
   string logoUrl, 
   string bannerUrl, 
   string description, 
   DateTimeOffset joinDate,
   DateTimeOffset subscriptionExpiredDate, 
   int ordersDelivered, 
   int upComingOrders)
  {
    Id = id;
    Name = name;
    Address = address;
    CityName = cityName;
    CityId = cityId;
    ContactNo = contactNo;
    OwnerName = ownerName;
    Status = status;
    CurrentSubscriptionPlanPrice = currentSubscriptionPlanPrice;
    CurrentSubscriptionPlanName = currentSubscriptionPlanName;
    LogoUrl = logoUrl;
    BannerUrl = bannerUrl;
    Description = description;
    JoinDate = joinDate;
    SubscriptionExpiredDate = subscriptionExpiredDate;
    OrdersDelivered = ordersDelivered;
    UpComingOrders = upComingOrders;
  }
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Address { get; set; }  = string.Empty;
  public string CityName { get; set; }  = string.Empty;
  public int CityId { get; set; } 
  public string ContactNo { get; set; }  = string.Empty;
  public string OwnerName { get; set; }  = string.Empty;
  public bool Status { get; set; } 
  public string CurrentSubscriptionPlanPrice { get; set; }  = string.Empty;
  public string CurrentSubscriptionPlanName { get; set; }  = string.Empty;
  public string LogoUrl { get; set; }  = string.Empty;
  public string BannerUrl { get; set; }  = string.Empty;
  public string Description { get; set; }  = string.Empty;
  public DateTimeOffset JoinDate { get; set; } 
  public DateTimeOffset SubscriptionExpiredDate { get; set; } 
  public int OrdersDelivered { get; set; }
  public int UpComingOrders { get; set; } 
}
