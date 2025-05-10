namespace Ordering.Api.Application.Commands;

// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
// In this case, its immutability is achieved by having all the setters as private
// plus only being able to update the data just once, when creating the object through its constructor.
// References on Immutable Commands:  
// http://cqrs.nu/Faq
// https://docs.spine3.org/motivation/immutability.html 
// http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
// https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

using Ordering.Api.Application.Models;
using Ordering.Api.Extensions;

[DataContract]
public class CreateOrderCommand
    : IRequest<bool>
{
    [DataMember]
    private readonly List<OrderItemDTO> _orderItems;

    [DataMember]
    public string UserId { get; private set; }

    [DataMember]
    public string UserName { get; private set; }

    [DataMember]
    public int ShopId { get; private set; }

    [DataMember]
    public string ShopName { get; private set; }

    [DataMember]
    public string City { get; private set; }

    [DataMember]
    public string Street { get; private set; }

    [DataMember]
    public string Location { get; private set; }

    [DataMember]
    public string District { get; private set; }

    [DataMember]
    public string Home { get; private set; }

    [DataMember]
    public string Tel { get; private set; }

    [DataMember]
    public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

    public CreateOrderCommand()
    {
        _orderItems = new List<OrderItemDTO>();
    }

    public CreateOrderCommand(List<BasketItem> basketItems, 
        string userId, 
        string userName, 
        int shopId,
        string shopName,
        string city, 
        string district, 
        string street, 
        string home,         
        string location,
        string tel)
    {
            _orderItems = basketItems.ToOrderItemsDTO().ToList();
            UserId = userId;
            UserName = userName;
            ShopId = shopId;
            ShopName = shopName;
            City = city;
            Home = home;
            District = district;
            Tel = tel;
            Street = street;
            Location = location;
            ShopId = basketItems.FirstOrDefault()!.ShopId;
            ShopName = basketItems.FirstOrDefault()!.ShopName;
    }
       
}


