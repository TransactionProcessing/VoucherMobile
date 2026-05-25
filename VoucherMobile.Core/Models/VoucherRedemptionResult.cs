namespace VoucherMobile.Core.Models;

public sealed class VoucherRedemptionResult
{
    public static VoucherRedemptionResult Success(Voucher voucher, decimal redeemedAmount, string message) => new()
    {
        IsSuccess = true,
        Voucher = voucher,
        RedeemedAmount = redeemedAmount,
        Message = message
    };

    public static VoucherRedemptionResult Failure(string message) => new()
    {
        IsSuccess = false,
        Message = message
    };

    public bool IsSuccess { get; init; }
    public Voucher? Voucher { get; init; }
    public decimal RedeemedAmount { get; init; }
    public required string Message { get; init; }
}
