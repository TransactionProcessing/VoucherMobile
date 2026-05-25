namespace VoucherMobile.Core.Models;

public sealed class Voucher
{
    public required string VoucherCode { get; init; }
    public required string CustomerEmail { get; init; }
    public decimal OriginalValue { get; init; }
    public decimal RemainingBalance { get; set; }
    public DateTime ExpiryDateUtc { get; init; }

    public bool IsExpired(DateTime? asOfUtc = null)
    {
        var now = asOfUtc ?? DateTime.UtcNow;
        return ExpiryDateUtc <= now;
    }
}
