using VoucherMobile.Core.Models;

namespace VoucherMobile.Core.Interfaces;

public interface IReceiptService
{
    Task SendReceiptAsync(string recipientEmail, VoucherRedemptionResult redemptionResult, CancellationToken cancellationToken = default);
}
