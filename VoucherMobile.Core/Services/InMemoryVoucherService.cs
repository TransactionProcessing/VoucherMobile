using VoucherMobile.Core.Interfaces;
using VoucherMobile.Core.Models;

namespace VoucherMobile.Core.Services;

public sealed class InMemoryVoucherService : IVoucherService
{
    private readonly IDictionary<string, Voucher> _vouchers;
    private readonly object _sync = new();

    public InMemoryVoucherService(IEnumerable<Voucher>? vouchers = null)
    {
        _vouchers = (vouchers ?? CreateDefaultVouchers())
            .ToDictionary(v => v.VoucherCode, StringComparer.OrdinalIgnoreCase);
    }

    public Task<Voucher?> GetVoucherAsync(string voucherCode, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(voucherCode))
        {
            return Task.FromResult<Voucher?>(null);
        }

        _vouchers.TryGetValue(voucherCode.Trim(), out var voucher);
        return Task.FromResult(voucher);
    }

    public Task<VoucherRedemptionResult> RedeemAsync(string voucherCode, TransactionDetails transaction, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (transaction.RedemptionAmount <= 0)
        {
            return Task.FromResult(VoucherRedemptionResult.Failure("Redemption amount must be greater than zero."));
        }

        lock (_sync)
        {
            if (!_vouchers.TryGetValue(voucherCode.Trim(), out var voucher))
            {
                return Task.FromResult(VoucherRedemptionResult.Failure("Voucher not found."));
            }

            if (voucher.IsExpired(transaction.TransactionTimeUtc))
            {
                return Task.FromResult(VoucherRedemptionResult.Failure("Voucher has expired."));
            }

            if (transaction.RedemptionAmount > voucher.RemainingBalance)
            {
                return Task.FromResult(VoucherRedemptionResult.Failure("Redemption amount exceeds remaining voucher balance."));
            }

            voucher.RemainingBalance -= transaction.RedemptionAmount;

            return Task.FromResult(
                VoucherRedemptionResult.Success(
                    voucher,
                    transaction.RedemptionAmount,
                    voucher.RemainingBalance == 0
                        ? "Voucher fully redeemed."
                        : "Voucher partially redeemed."));
        }
    }

    private static IEnumerable<Voucher> CreateDefaultVouchers()
    {
        yield return new Voucher
        {
            VoucherCode = "VOUCHER-001",
            CustomerEmail = "customer@example.com",
            OriginalValue = 100m,
            RemainingBalance = 100m,
            ExpiryDateUtc = DateTime.UtcNow.AddDays(30)
        };
    }
}
