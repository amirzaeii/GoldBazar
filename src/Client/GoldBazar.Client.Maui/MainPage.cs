using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebView.Maui;

namespace GoldBazar.Client.Maui;

public partial class  MainPage : ContentPage
{
    public MainPage()
    {
        var blazorWebView = new BlazorWebView
        {
            HostPage = "wwwroot/index.html",
        };
        blazorWebView.RootComponents.Add(new RootComponent
        {
            Selector = "#app",
            ComponentType = typeof(App)
        });

        Content = blazorWebView;
    }
}
