using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using VoucherMobile.BusinessLogic.ViewModels;

namespace VoucherMobile.App.Pages;

public partial class LoginPage : ContentPage
{
    private LoginPageViewModel ViewModel => (LoginPageViewModel)this.BindingContext;

    public LoginPage()
    {
        InitializeComponent();
        this.BindingContext = MauiProgram.Container.Services.GetRequiredService<LoginPageViewModel>();
        this.ViewModel.PropertyChanged += this.OnViewModelPropertyChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.ViewModel.ResetForDisplay();
    }

    private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(LoginPageViewModel.IsAuthenticated) || this.ViewModel.IsAuthenticated == false)
        {
            return;
        }

        String userName = Uri.EscapeDataString(this.ViewModel.AuthenticatedUserName ?? String.Empty);
        await Shell.Current.GoToAsync($"///home?username={userName}");
    }
}
