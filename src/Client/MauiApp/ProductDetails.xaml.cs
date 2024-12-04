using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Syncfusion.Maui.Inputs;
using System.Collections.ObjectModel;



namespace MauiApp1;

public partial class ProductDetails : ContentPage
{
    public ObservableCollection<P1> Prods { get; set; }
    public List<SfRotatorItem> CollectionOfItems { get; set; }
    public Productes Product { get; set; }
    public ObservableCollection<Productes> Products { get; set; }
    public ProductDetails()
    {
        InitializeComponent();
        // Products
        Prods = new ObservableCollection<P1>
            {
                new P1
                {
                    ProductImage = "Resources/Images/gwara.svg",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    OfferBadge = "Resources/Icons/offer.svg"
                },
                new P1
                {
                    ProductImage = "Resources/Images/gwara2.svg",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg"
                },
                new P1
                {
                    ProductImage = "Resources/Images/gwara3.png",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg"
                },
                new P1
                {
                    ProductImage = "Resources/Images/gwara.svg",
                    ProductTitle = "گوارەی عەیارە ١٨",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    OfferBadge = "Resources/Icons/offer.svg"
                }
            };

        CollectionOfItems = new List<SfRotatorItem>
        {
            new SfRotatorItem { MediaSource = "item1.jpg", Type = "Image", IsImage = true },
            new SfRotatorItem { MediaSource = "item2.jpg", Type = "Image", IsImage = true },
            new SfRotatorItem { MediaSource = "item3.jpg", Type = "Image", IsImage = true },
            new SfRotatorItem { MediaSource = MediaSource.FromResource("vid.mp4"), Type = "Video", IsVideo = true }
        };
        Products = new ObservableCollection<Productes>
        {
            new Productes
            {
                Name = "گوارەی ئەستێرە",
                CurrentPrice = 179,
                OriginalPrice = 199,
                Karate = "18",
                Weight = 43,
                Quantity = 21,
                Rating = 4.0,
                TotalReviews = 122,
                ShopName = "ئەڤیتا ستۆر",
                Location = "سلێمانی, به‌رامبەر سه‌را",
                ShopLogo = "dotnet_bot.png",
                Length = 43,
                Thickness = 8.55,
                Description = new List<string>
                {
                    "ستایلی کلاسیک",
                    "پۆلێش کراو",
                    "پەخشی دەستکرد",
                    "تاڵۆنی زەرد"
                }
            },
             new Productes
            {
                Name = "گوارەی ئەستێرە",
                CurrentPrice = 179,
                OriginalPrice = 199,
                Karate = "18",
                Weight = 43,
                Quantity = 21,
                Rating = 4.0,
                TotalReviews = 122,
                ShopName = "ئەڤیتا ستۆر",
                Location = "سلێمانی, به‌رامبەر سه‌را",
                ShopLogo = "dotnet_bot.png",
                Length = 43,
                Thickness = 8.55,
                Description = new List<string>
                {
                    "ستایلی کلاسیک",
                    "پۆلێش کراو",
                    "پەخشی دەستکرد",
                    "تاڵۆنی زەرد"
                }
            },
              new Productes
            {
                Name = "گوارەی ئەستێرە",
                CurrentPrice = 179,
                OriginalPrice = 199,
                Karate = "18",
                Weight = 43,
                Quantity = 21,
                Rating = 4.0,
                TotalReviews = 122,
                ShopName = "ئەڤیتا ستۆر",
                Location = "سلێمانی, به‌رامبەر سه‌را",
                ShopLogo = "dotnet_bot.png",
                Length = 43,
                Thickness = 8.55,
                Description = new List<string>
                {
                    "ستایلی کلاسیک",
                    "پۆلێش کراو",
                    "پەخشی دەستکرد",
                    "تاڵۆنی زەرد"
                }
            }
        };


        // Product data (example in Kurdish)
        Product = new Productes
        {
            Name = "گوارەی ئەستێرە",
            CurrentPrice = 179,
            OriginalPrice = 199,
            Karate = "18 ",
            Weight = 43,
            Quantity = 21,
            Rating = 4.0,
            TotalReviews = 122,
            ShopName = "ئەڤیتا ستۆر",
            Location = "سلێمانی, به‌رامبەر سه‌را",
            ShopLogo = "dotnet_bot.png", // Replace with actual logo path
            Length = 43, // cm
            Thickness = 8.55, // mm
            Description = new List<string>
        {
            "ستایلی کلاسیک",
            "پۆلێش کراو",
            "پەخشی دەستکرد",
            "تاڵۆنی زەرد"
        }
        };

        BindingContext = this;
    }
    private void OnAddToCartClicked(object sender, EventArgs e)
    { }
}



public class SfRotatorItem
{
    public object MediaSource { get; set; }
    public string Type { get; set; }
    public bool IsImage { get; set; } = false;
    public bool IsVideo { get; set; } = false;
}

public class Productes
{
    // Main Attributes
    public string Name { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public string Karate { get; set; } // Gold Purity (e.g., 18K)
    public double Weight { get; set; } // In grams
    public int Quantity { get; set; } // Number of items available
    public double Rating { get; set; } // Out of 5
    public int TotalReviews { get; set; }
    public string ShopName { get; set; }
    public string Location { get; set; }
    public string ShopLogo { get; set; } // URL or path to the logo

    // Dimensions
    public double Length { get; set; } // In cm
    public double Thickness { get; set; } // In mm

    // Description Points
    public List<string> Description { get; set; }

    // Method to Calculate Discount Percentage
    public decimal CalculateDiscount()
    {
        if (OriginalPrice > 0)
        {
            return (OriginalPrice - CurrentPrice) / OriginalPrice * 100;
        }
        return 0;
    }
}
public class P1
{
    public string ProductImage { get; set; }
    public string ProductTitle { get; set; }
    public string Price { get; set; }
    public string FavoriteIcon { get; set; }
    public string OfferBadge { get; set; } // Optional offer badge
}

