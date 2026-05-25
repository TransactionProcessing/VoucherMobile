namespace VoucherMobile.Core.Models;

public sealed class TransactionDetails
{
    public required string MerchantId { get; init; }
    public required string TransactionReference { get; init; }
    public required decimal RedemptionAmount { get; init; }
    public required DateTime TransactionTimeUtc { get; init; }
}
