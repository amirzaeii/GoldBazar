namespace GoldBazar.Shared.DTOs;
public class BasketItemDTO
{
    public required string Id { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    //public int Quantity { get; set; }'
    public Action<BasketItemDTO, int>? OnQuantityChanged { get; set; }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity == value) return;
            _quantity = value;
            // fire the page�s handler
            OnQuantityChanged?.Invoke(this, value);
        }
    }
    public string StoreName { get; set; } = default!;
    public int StoreId { get; set; }

}
