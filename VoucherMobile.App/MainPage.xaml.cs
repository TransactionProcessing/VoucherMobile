using VoucherMobile.App.ViewModels;

namespace VoucherMobile.App;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainPageViewModel();
        BindingContext = _viewModel;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await _viewModel.Login.LoginAsync();
    }

    private async void OnLoadVoucherClicked(object sender, EventArgs e)
    {
        await _viewModel.Redemption.ScanVoucherAsync();
    }

    private async void OnRedeemClicked(object sender, EventArgs e)
    {
        await _viewModel.Redemption.RedeemAsync();
    }
}
