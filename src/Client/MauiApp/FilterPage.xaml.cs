using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
namespace MauiApp1;

public partial class FilterPage : ContentPage, INotifyPropertyChanged
{
    public ObservableCollection<Product> Products { get; set; }
    private ObservableCollection<string> filterChips;

    public FilterPage()
    {
        InitializeComponent();

        // Initialize collections
        filterChips = new ObservableCollection<string> { "گوارە", "ملوانکە", "عەڵقە", "بازنە" };
        RenderChips();

        Products = new ObservableCollection<Product>
        {
            new Product { ProductImage = "Resources/Images/gwara.svg", ProductTitle = "گوارەی عەیارە ۲۴", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg", OfferBadge = "Resources/Icons/offer.svg" },
            new Product { ProductImage = "Resources/Images/gwara2.svg", ProductTitle = "گوارەی عەیارە ۲٤", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg" },
             new Product { ProductImage = "Resources/Images/gwara.svg", ProductTitle = "گوارەی عەیارە ۲۴", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg", OfferBadge = "Resources/Icons/offer.svg" },
            new Product { ProductImage = "Resources/Images/gwara2.svg", ProductTitle = "گوارەی عەیارە ۲٤", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg" },
             new Product { ProductImage = "Resources/Images/gwara2.svg", ProductTitle = "گوارەی عەیارە ۲٤", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg" },
             new Product { ProductImage = "Resources/Images/gwara.svg", ProductTitle = "گوارەی عەیارە ۲۴", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg", OfferBadge = "Resources/Icons/offer.svg" },
            new Product { ProductImage = "Resources/Images/gwara2.svg", ProductTitle = "گوارەی عەیارە ۲٤", Price = "٢٣٤ دۆلار", FavoriteIcon = "Resources/Icons/Heart.svg" },
        };

        BindingContext = this;
    }

    // Render Chips in FilterChipsContainer
    private void RenderChips()
    {
        FilterChipsContainer.Children.Clear();
        foreach (var chipLabel in filterChips)
        {
            var frame = new Frame
            {
                BackgroundColor = Color.FromArgb("#f0f7ff"),
                CornerRadius = 20,
                HasShadow = false,
                Padding = new Thickness(5, 8),
                Margin = new Thickness(0, 0, 8, 8)
            };

            var chipLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 2,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var removeButton = new Label
            {
                Text = "×",
                TextColor = Color.FromArgb("#0066ff"),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => RemoveChip(chipLabel);
            removeButton.GestureRecognizers.Add(tapGestureRecognizer);

            var label = new Label
            {
                Text = chipLabel,
                FontSize = 14,
                TextColor = Color.FromArgb("#0066ff"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };

            chipLayout.Children.Add(removeButton);
            chipLayout.Children.Add(label);
            frame.Content = chipLayout;

            FilterChipsContainer.Children.Add(frame);
        }
    }

    private void RemoveChip(string chipLabel)
    {
        if (filterChips.Contains(chipLabel))
        {
            filterChips.Remove(chipLabel);
            RenderChips();
        }
    }

    private async void OnBackButtonTapped(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private void OnFilterTriggerTapped(object sender, EventArgs e)
    {
        var popup = new filteringpopup(); // Assuming 'filteringpopup' is your Popup class name
        this.ShowPopup(popup); // Show the popup
    }


    // Helper method to set properties and notify of changes
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class Product
    {
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string Price { get; set; }
        public string FavoriteIcon { get; set; }
        public string OfferBadge { get; set; }
    }
}