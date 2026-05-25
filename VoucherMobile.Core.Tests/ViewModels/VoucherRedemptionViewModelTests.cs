using VoucherMobile.Core.Models;
using VoucherMobile.Core.Services;
using VoucherMobile.Core.ViewModels;

namespace VoucherMobile.Core.Tests.ViewModels;

public sealed class VoucherRedemptionViewModelTests
{
    [Fact]
    public async Task RedeemAsync_SendsReceipt_WhenRedemptionSucceeds()
    {
        var audit = new List<string>();
        var voucherService = new InMemoryVoucherService(new[]
        {
            new Voucher
            {
                VoucherCode = "SCAN-001",
                CustomerEmail = "customer@example.com",
                OriginalValue = 60m,
                RemainingBalance = 60m,
                ExpiryDateUtc = DateTime.UtcNow.AddDays(3)
            }
        });

        var vm = new VoucherRedemptionViewModel(voucherService, new InMemoryReceiptService(audit))
        {
            VoucherCode = "SCAN-001",
            MerchantId = "MERCHANT-1",
            TransactionReference = "TX-100",
            RedemptionAmount = 25m
        };

        var scanSuccess = await vm.ScanVoucherAsync();
        var redeemResult = await vm.RedeemAsync();

        Assert.True(scanSuccess);
        Assert.True(redeemResult.IsSuccess);
        Assert.Single(audit);
        Assert.Contains("customer@example.com", audit[0]);
        Assert.Equal("Voucher partially redeemed.", vm.StatusMessage);
    }
}
