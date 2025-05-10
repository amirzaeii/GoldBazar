using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Infrastructure.Models;

public class Item
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Caption is required.")]
    [StringLength(255, ErrorMessage = "Caption cannot exceed 255 characters.")]
    public string Caption { get; set; } = string.Empty;
    public bool Status { get; set; }    
    public decimal Discount { get; set; }
    public decimal EligibleChangePriceRang { get; set; }

    [Required(ErrorMessage = "Weight is required.")]
    public decimal Weight { get; set; }        
    public TypeEnum Type { get; set; } = default!; 
    public Category Category { get; set; } = default!; 
    public int CategoryId { get; set; }
    public int MaterialId { get; set; } // FK to Material
    [ForeignKey("MaterialId")]
    public Material Material { get; set; } = default!; // Navigation property

    public int MetalId { get; set; } 
    [ForeignKey("MetalId")]
    public Metal Metal { get; set; } = default!; // Navigation property

    public int OccasionId { get; set; } // FK to Occasion
    [ForeignKey("OccasionId")]
    public Occassion Occassion { get; set; } = default!; // Navigation property
    public int StyleId { get; set; } // FK to Style
    [ForeignKey("StyleId")]
    public Style Style { get; set; } = default!; // Navigation property

    public string? Description { get; set; }

    [Required(ErrorMessage = "Cost Per Gram is required.")]
    public decimal CostPerGram { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public int ShopId { get; set; } // FK to Shop
    [ForeignKey("ShopId")]
    public Shop Shop { get; set; } = default!; // Navigation property
    public int ManufactureId { get; set; }
    public Manufacture Manufacture { get; set; } = default!; // Navigation property
    // Added 
    [Range(0, int.MaxValue, ErrorMessage = "Size cannot be negative.")]
    public int Size { get; set; } // Optional or required, depending on business rules
    public string? MainPhoto { get; set; }
    // Quantity in stock
    public int AvailableStock { get; set; }
    // Available stock at which we should reorder
    public int RestockThreshold { get; set; }

    // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
    public int MaxStockThreshold { get; set; }
    /// <summary>
    /// Decrements the quantity of a particular item in inventory and ensures the restockThreshold hasn't
    /// been breached. If so, a RestockRequest is generated in CheckThreshold. 
    /// 
    /// If there is sufficient stock of an item, then the integer returned at the end of this call should be the same as quantityDesired. 
    /// In the event that there is not sufficient stock available, the method will remove whatever stock is available and return that quantity to the client.
    /// In this case, it is the responsibility of the client to determine if the amount that is returned is the same as quantityDesired.
    /// It is invalid to pass in a negative number. 
    /// </summary>
    /// <param name="quantityDesired"></param>
    /// <returns>int: Returns the number actually removed from stock. </returns>
    /// 
    public int RemoveStock(int quantityDesired)
    {
        if (AvailableStock == 0)
        {
            throw new CatalogDomainException($"Empty stock, product item {Caption} is sold out");
        }

        if (quantityDesired <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }

        int removed = Math.Min(quantityDesired, this.AvailableStock);

        this.AvailableStock -= removed;

        return removed;
    }

    /// <summary>
    /// Increments the quantity of a particular item in inventory.
    /// <param name="quantity"></param>
    /// <returns>int: Returns the quantity that has been added to stock</returns>
    /// </summary>
    public int AddStock(int quantity)
    {
        int original = this.AvailableStock;

        // The quantity that the client is trying to add to stock is greater than what can be physically accommodated in the Warehouse
        if ((AvailableStock + quantity) > MaxStockThreshold)
        {
            // For now, this method only adds new units up maximum stock threshold. In an expanded version of this application, we
            //could include tracking for the remaining units and store information about overstock elsewhere. 
            AvailableStock += MaxStockThreshold - AvailableStock;
        }
        else
        {
            AvailableStock += quantity;
        }

        return AvailableStock - original;
    }

    public enum TypeEnum
    {
        [Display(Name = "Women")]
        Women = 1,
        [Display(Name = "Men")]
        Men = 2,
        [Display(Name = "Kids")]
        Kids = 3 
    }
}