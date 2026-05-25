namespace VoucherMobile.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(VoucherLookupPage), typeof(VoucherLookupPage));
        Routing.RegisterRoute(nameof(RedemptionPage), typeof(RedemptionPage));
    }
}
