using VoucherMobile.Core.Services;
using VoucherMobile.Core.ViewModels;

namespace VoucherMobile.App.ViewModels;

public sealed class MainPageViewModel
{
    public MainPageViewModel()
    {
        Login = new LoginViewModel(new InMemoryAuthenticationService());

        var voucherService = new InMemoryVoucherService();
        var receiptService = new InMemoryReceiptService();
        Redemption = new VoucherRedemptionViewModel(voucherService, receiptService);
    }

    public LoginViewModel Login { get; }

    public VoucherRedemptionViewModel Redemption { get; }
}
