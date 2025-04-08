using System.Globalization;

namespace WebComponent.Components.LanguageSelection;

public partial class LanguageSelection
{
    private List<CultureInfo> _cultures = new()
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-IQ"),
        new CultureInfo("ku-Arab")
    };
    protected override void OnInitialized()
    {
        Culture = CultureInfo.CurrentCulture;
    }

    private CultureInfo Culture
    {
        get
        {
            return CultureInfo.CurrentCulture;
        }
        set
        {
            if (CultureInfo.CurrentCulture != value)
            {
                var uri = new Uri(Navigation.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var cultureEscaped = Uri.EscapeDataString(value.Name);
                var uriEscaped = Uri.EscapeDataString(uri);

                Navigation.NavigateTo($"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                    forceLoad: true);
            }
        }
    }
}