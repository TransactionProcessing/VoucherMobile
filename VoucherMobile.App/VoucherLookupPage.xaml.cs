namespace VoucherMobile.App;

public partial class VoucherLookupPage : ContentPage
{
    private AppState State => ((App)Application.Current!).State;

    public VoucherLookupPage()
    {
        InitializeComponent();
        BindingContext = State;
    }

    private async void OnLoadVoucherClicked(object sender, EventArgs e)
    {
        if (await State.Redemption.ScanVoucherAsync())
        {
            await Shell.Current!.GoToAsync(nameof(RedemptionPage));
        }
    }
}
