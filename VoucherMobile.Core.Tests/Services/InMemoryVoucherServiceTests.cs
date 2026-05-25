using VoucherMobile.Core.Models;
using VoucherMobile.Core.Services;

namespace VoucherMobile.Core.Tests.Services;

public sealed class InMemoryVoucherServiceTests
{
    [Fact]
    public async Task RedeemAsync_Fails_WhenVoucherExpired()
    {
        var service = new InMemoryVoucherService(new[]
        {
            new Voucher
            {
                VoucherCode = "EXPIRED-001",
                CustomerEmail = "expired@example.com",
                OriginalValue = 100m,
                RemainingBalance = 100m,
                ExpiryDateUtc = DateTime.UtcNow.AddDays(-1)
            }
        });

        var result = await service.RedeemAsync("EXPIRED-001", new TransactionDetails
        {
            MerchantId = "MERCHANT-1",
            TransactionReference = "TX-1",
            RedemptionAmount = 10m,
            TransactionTimeUtc = DateTime.UtcNow
        });

        Assert.False(result.IsSuccess);
        Assert.Equal("Voucher has expired.", result.Message);
    }

    [Fact]
    public async Task RedeemAsync_Fails_WhenAmountExceedsBalance()
    {
        var service = new InMemoryVoucherService(new[]
        {
            new Voucher
            {
                VoucherCode = "VALID-001",
                CustomerEmail = "valid@example.com",
                OriginalValue = 50m,
                RemainingBalance = 20m,
                ExpiryDateUtc = DateTime.UtcNow.AddDays(10)
            }
        });

        var result = await service.RedeemAsync("VALID-001", new TransactionDetails
        {
            MerchantId = "MERCHANT-1",
            TransactionReference = "TX-2",
            RedemptionAmount = 25m,
            TransactionTimeUtc = DateTime.UtcNow
        });

        Assert.False(result.IsSuccess);
        Assert.Equal("Redemption amount exceeds remaining voucher balance.", result.Message);
    }

    [Fact]
    public async Task RedeemAsync_SupportsPartialAndFullRedemption()
    {
        var service = new InMemoryVoucherService(new[]
        {
            new Voucher
            {
                VoucherCode = "VALID-002",
                CustomerEmail = "valid@example.com",
                OriginalValue = 100m,
                RemainingBalance = 100m,
                ExpiryDateUtc = DateTime.UtcNow.AddDays(10)
            }
        });

        var partial = await service.RedeemAsync("VALID-002", new TransactionDetails
        {
            MerchantId = "MERCHANT-1",
            TransactionReference = "TX-3",
            RedemptionAmount = 30m,
            TransactionTimeUtc = DateTime.UtcNow
        });

        Assert.True(partial.IsSuccess);
        Assert.Equal(70m, partial.Voucher!.RemainingBalance);
        Assert.Equal("Voucher partially redeemed.", partial.Message);

        var full = await service.RedeemAsync("VALID-002", new TransactionDetails
        {
            MerchantId = "MERCHANT-1",
            TransactionReference = "TX-4",
            RedemptionAmount = 70m,
            TransactionTimeUtc = DateTime.UtcNow
        });

        Assert.True(full.IsSuccess);
        Assert.Equal(0m, full.Voucher!.RemainingBalance);
        Assert.Equal("Voucher fully redeemed.", full.Message);
    }
}
