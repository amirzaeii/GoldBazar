using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<PromotionSlide> PromotionSlides { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<NavItem> NavItems { get; set; }
        public ICommand NavigateToProductDetailsCommand { get; }

        [Obsolete]
        public MainPage()
        {
            InitializeComponent();
            // Initialize the command
            NavigateToProductDetailsCommand = new Command<Product>(NavigateToProductDetails);
            // Initializing PromotionSlides
            PromotionSlides = new ObservableCollection<PromotionSlide>
            {
                new PromotionSlide
                {
                    ImageSource = "Resources/Images/banner.svg",
                    SlideTitle = "داشکاندن ٢٠٪",
                    SlideSubtitle = "لە ڕیشتەکانی سێرکان"
                },
                new PromotionSlide
                {
                    ImageSource = "Resources/Images/banner.svg",
                    SlideTitle = "پێشنیاری تایبەت",
                    SlideSubtitle = "بەرهەمە نوێیەکان"
                },
                new PromotionSlide
                {
                    ImageSource = "Resources/Images/banner.svg",
                    SlideTitle = "کۆلێکشنی نوێ",
                    SlideSubtitle = "بەهاری ٢٠٢٤"
                }
            };

            // Initializing Categories
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Name = "پشتێن",
                    ImageSource = "Resources/Images/pshten.ico"
                },
                new Category
                {
                    Name = "گوارە",
                    ImageSource = "Resources/Images/gweee.ico"
                },
                new Category
                {
                    Name = "بازنە",
                    ImageSource = "Resources/Images/dastband.ico"
                },
                new Category
                {
                    Name = "ملوانکە",
                    ImageSource = "Resources/Images/mlwanka.ico"
                },
                new Category
                {
                    Name = "ئەنگوستیلە",
                    ImageSource = "Resources/Images/alqa.ico"
                },
                new Category
                {
                    Name = "کاتژمێر",
                    ImageSource = "Resources/Images/saat.ico"
                },
                new Category
                {
                    Name = "تاج",
                    ImageSource = "Resources/Images/taj.ico"
                },
                new Category
                {
                    Name = "پاوانه‌",
                    ImageSource = "Resources/Images/pawana.ico"
                },
                new Category
                {
                    Name = "لوتەوانە",
                    ImageSource = "Resources/Images/lwtawana.ico"
                },
                new Category
                {
                    Name = "مەدالیا‌",
                    ImageSource = "Resources/Images/madalia.ico"
                }
            };

            // Products
            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    ProductImage = "Resources/Images/gwara.svg",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "$٢٣٤" ,
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    OfferBadge = "Resources/Icons/offer.svg",
                    shopname = "زەڕەنگەری ھەرێم"
                },
                new Product
                {
                    ProductImage = "Resources/Images/gwara2.svg",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "$٢٣٤",
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    shopname = "زەڕەنگەری ژیر"
                },
                new Product
                {
                    ProductImage = "Resources/Images/gwara3.png",
                    ProductTitle = "گوارەی عەیارە ۲۴",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    shopname = "زەڕەنگەری ھەرێم"
                },
                new Product
                {
                    ProductImage = "Resources/Images/gwara.svg",
                    ProductTitle = "گوارەی عەیارە ١٨",
                    Price = "٢٣٤ دۆلار",
                    FavoriteIcon = "Resources/Icons/Heart.svg",
                    OfferBadge = "Resources/Icons/offer.svg",
                    shopname = "زەڕەنگەری ژیر"
                }
            };

            // Set BindingContext after initializing the collections
            BindingContext = this;
            // Start automatic scrolling
            StartAutoScroll();

        }

        private async void NavigateToProductDetails(Product product)
        {
            if (product != null)
            {
                await Navigation.PushAsync(new ProductDetails());
            }
        }
        private async void OnFilterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilterPage());
        }

        // Event handler for heart button
        private void OnHeartClicked(object sender, EventArgs e)
        {
            // TODO: Add your logic for the heart button here
            DisplayAlert("Favorites", "Heart button clicked", "OK");
        }

        private void OnNavItemSelected(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var selectedNavItem = (NavItem)button.BindingContext;

            foreach (var item in NavItems)
            {
                item.IsSelected = item == selectedNavItem;
            }
        }

        public class NavItem : INotifyPropertyChanged
        {
            private bool isSelected;

            public string IconOutline { get; set; }
            public string IconFill { get; set; }
            public string Label { get; set; }

            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        OnPropertyChanged(nameof(IsSelected));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnCategoryClicked(object sender, EventArgs e)
        {
            // Handle category click
        }

        [Obsolete]
        private void StartAutoScroll()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                if (PromotionCarousel.ItemsSource != null && PromotionSlides.Count > 1)
                {
                    int nextIndex = (PromotionCarousel.Position + 1) % PromotionSlides.Count;
                    PromotionCarousel.Position = nextIndex;
                }
                // Return true to keep the timer running
                return true;
            });
        }
    }

    public class PromotionSlide
    {
        public string ImageSource { get; set; }
        public string SlideTitle { get; set; }
        public string SlideSubtitle { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
    public class Product
    {
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string Price { get; set; }
        public string FavoriteIcon { get; set; }
        public string OfferBadge { get; set; } // Optional offer badge
        public string shopname { get; set; }
    }

}