using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace HybridApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		        builder.ConfigureLifecycleEvents(lifecycle =>
        {
#if iOS || Mac
             var handlerType = typeof(BlazorWebViewHandler);
             var field = handlerType.GetField("AppOriginUri", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AppOriginUri field not found");
             field.SetValue(null, new Uri("app://localhost/"));
            
            lifecycle.AddiOS(ios =>
            {
                bool HandleAppLink(Foundation.NSUserActivity? userActivity)
                {
                    if (userActivity is not null && userActivity.ActivityType == Foundation.NSUserActivityType.BrowsingWeb && userActivity.WebPageUrl is not null)
                    {
                        var url = $"{userActivity.WebPageUrl.Path}?{userActivity.WebPageUrl.Query}";

                        var _ = Routes.OpenUniversalLink(url);

                        return true;
                    }

                    return false;
                }

                ios.FinishedLaunching((app, data)
                    => HandleAppLink(app.UserActivity));

                ios.ContinueUserActivity((app, userActivity, handler)
                    => HandleAppLink(userActivity));

                if (OperatingSystem.IsIOSVersionAtLeast(13) || OperatingSystem.IsMacCatalystVersionAtLeast(13))
                {
                    ios.SceneWillConnect((scene, sceneSession, sceneConnectionOptions)
                        => HandleAppLink(sceneConnectionOptions.UserActivities.ToArray()
                            .FirstOrDefault(a => a.ActivityType == Foundation.NSUserActivityType.BrowsingWeb)));

                    ios.SceneContinueUserActivity((scene, userActivity)
                        => HandleAppLink(userActivity));
                }
            });
#endif
        });
        //localization
        builder.Services.AddLocalization();
		builder.AddApplicationServices();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		var app =  builder.Build();

        
		return app;
	}
}
