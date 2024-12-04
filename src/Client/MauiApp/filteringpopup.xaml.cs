using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Views;

namespace MauiApp1;

public partial class filteringpopup : CommunityToolkit.Maui.Views.Popup, INotifyPropertyChanged
{
    public ObservableCollection<Governorate> Governorates { get; set; }
    public ObservableCollection<ProductType> ProductTypes { get; set; }
    public ObservableCollection<Karate> Karates { get; set; }
    public ObservableCollection<WeightProduct> WeightProducts { get; set; }
    public ObservableCollection<SizeProduct> SizeProducts { get; set; }

    public filteringpopup()
    {
        InitializeComponent();

        Governorates = new ObservableCollection<Governorate>
        {
            new Governorate
            {
                Name = "سلیمانی",
                Districts = new ObservableCollection<District>
                {
                    new District { Name = "سه‌رچنار" },
                    new District { Name = "باخان" },
                    new District { Name = "سابونکاران" },
                    new District { Name = "گۆیژە" }
                }
            },
            new Governorate
            {
                Name = "هەولێر",
                Districts = new ObservableCollection<District>
                {
                    new District { Name = "عه‌نكاوه‌" },
                    new District { Name = "شوڕش" },
                    new District { Name = "نازناز" },
                    new District { Name = "سیتاقان" }
                }
            }
        };

        ProductTypes = new ObservableCollection<ProductType>
        {
            new ProductType { Name = "ملوانکە" },
            new ProductType { Name = "گوارە" },
            new ProductType { Name = "کاتژمێری دەستی" }
        };

        Karates = new ObservableCollection<Karate>
        {
            new Karate { Name = "١٨" },
            new Karate { Name = "٢١" },
            new Karate { Name = "٢٤" }
        };

        WeightProducts = new ObservableCollection<WeightProduct>
        {
            new WeightProduct { Name = "٠-٦" },
            new WeightProduct { Name = "٦-١٢" }
        };

        SizeProducts = new ObservableCollection<SizeProduct>
        {
            new SizeProduct { Name = "٢٢ ملم" },
            new SizeProduct { Name = "٢٤ ملم" }
        };

        BindingContext = this;

        // Subscribe to changes in properties
        SubscribeToPropertyChanges();

        // Update selected counts initially
        UpdateSelectedCounts();



    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
       
    }

    private void SubscribeToPropertyChanges()
    {
        foreach (var productType in ProductTypes)
        {
            productType.PropertyChanged += (s, e) => UpdateSelectedCounts();
        }

        foreach (var governorate in Governorates)
        {
            foreach (var district in governorate.Districts)
            {
                district.PropertyChanged += (s, e) => UpdateSelectedCounts();
            }
        }

        foreach (var karate in Karates)
        {
            karate.PropertyChanged += (s, e) => UpdateSelectedCounts();
        }

        foreach (var weightProduct in WeightProducts)
        {
            weightProduct.PropertyChanged += (s, e) => UpdateSelectedCounts();
        }

        foreach (var sizeProduct in SizeProducts)
        {
            sizeProduct.PropertyChanged += (s, e) => UpdateSelectedCounts();
        }
    }

    private void UpdateSelectedCounts()
    {
        GovernorateSelectedCount = Governorates.Sum(g => g.Districts.Count(d => d.IsSelected));
        ProductTypeSelectedCount = ProductTypes.Count(p => p.IsSelected);
        KarateSelectedCount = Karates.Count(k => k.IsSelected);
        WeightProductSelectedCount = WeightProducts.Count(w => w.IsSelected);
        SizeProductSelectedCount = SizeProducts.Count(s => s.IsSelected);
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        foreach (var productType in ProductTypes) productType.IsSelected = false;
        foreach (var governorate in Governorates)
            foreach (var district in governorate.Districts)
                district.IsSelected = false;
        foreach (var karate in Karates) karate.IsSelected = false;
        foreach (var weightProduct in WeightProducts) weightProduct.IsSelected = false;
        foreach (var sizeProduct in SizeProducts) sizeProduct.IsSelected = false;

        UpdateSelectedCounts();
    }

    private int _governorateSelectedCount;
    public int GovernorateSelectedCount
    {
        get => _governorateSelectedCount;
        set { _governorateSelectedCount = value; OnPropertyChanged(); }
    }

    private int _productTypeSelectedCount;
    public int ProductTypeSelectedCount
    {
        get => _productTypeSelectedCount;
        set { _productTypeSelectedCount = value; OnPropertyChanged(); }
    }

    private int _karateSelectedCount;
    public int KarateSelectedCount
    {
        get => _karateSelectedCount;
        set { _karateSelectedCount = value; OnPropertyChanged(); }
    }

    private int _weightProductSelectedCount;
    public int WeightProductSelectedCount
    {
        get => _weightProductSelectedCount;
        set { _weightProductSelectedCount = value; OnPropertyChanged(); }
    }

    private int _sizeProductSelectedCount;
    public int SizeProductSelectedCount
    {
        get => _sizeProductSelectedCount;
        set { _sizeProductSelectedCount = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Governorate : INotifyPropertyChanged
{
    public string Name { get; set; }
    public ObservableCollection<District> Districts { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}

public class District : INotifyPropertyChanged
{
    private bool _isSelected;
    public string Name { get; set; }
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ProductType : INotifyPropertyChanged
{
    private bool _isSelected;
    public string Name { get; set; }
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Karate : INotifyPropertyChanged
{
    private bool _isSelected;
    public string Name { get; set; }
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class WeightProduct : INotifyPropertyChanged
{
    private bool _isSelected;
    public string Name { get; set; }
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class SizeProduct : INotifyPropertyChanged
{
    private bool _isSelected;
    public string Name { get; set; }
    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
