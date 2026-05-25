using VoucherMobile.Core.Services;
using VoucherMobile.Core.ViewModels;

namespace VoucherMobile.App;

public sealed class AppState
{
    public AppState()
    {
        Login = new LoginViewModel(new InMemoryAuthenticationService());

        var voucherService = new InMemoryVoucherService();
        var receiptService = new InMemoryReceiptService();
        Redemption = new VoucherRedemptionViewModel(voucherService, receiptService);
    }

    public LoginViewModel Login { get; }

    public VoucherRedemptionViewModel Redemption { get; }
}
