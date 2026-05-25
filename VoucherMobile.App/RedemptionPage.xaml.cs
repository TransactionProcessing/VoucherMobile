namespace VoucherMobile.App;

public partial class RedemptionPage : ContentPage
{
    private AppState State => ((App)Application.Current!).State;

    public RedemptionPage()
    {
        InitializeComponent();
        BindingContext = State;
    }

    private async void OnRedeemClicked(object sender, EventArgs e)
    {
        await State.Redemption.RedeemAsync();
    }
}
