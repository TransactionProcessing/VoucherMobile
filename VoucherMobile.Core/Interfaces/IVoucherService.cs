using VoucherMobile.Core.Models;

namespace VoucherMobile.Core.Interfaces;

public interface IVoucherService
{
    Task<Voucher?> GetVoucherAsync(string voucherCode, CancellationToken cancellationToken = default);
    Task<VoucherRedemptionResult> RedeemAsync(string voucherCode, TransactionDetails transaction, CancellationToken cancellationToken = default);
}
