using Microsoft.Extensions.DependencyInjection;
using VoucherMobile.App.Pages;
using VoucherMobile.App.Pages.AppHome;
using VoucherMobile.BusinessLogic.Services;
using VoucherMobile.BusinessLogic.ViewModels;

namespace VoucherMobile.App.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder ConfigureAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IAuthenticationService, InMemoryAuthenticationService>();
        return builder;
    }

    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LoginPageViewModel>();
        return builder;
    }

    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        return builder;
    }
}
