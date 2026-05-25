using VoucherMobile.Core.Interfaces;
using VoucherMobile.Core.Models;

namespace VoucherMobile.Core.Services;

public sealed class InMemoryReceiptService : IReceiptService
{
    private readonly IList<string> _sentReceiptAudit;

    public InMemoryReceiptService(IList<string>? sentReceiptAudit = null)
    {
        _sentReceiptAudit = sentReceiptAudit ?? new List<string>();
    }

    public Task SendReceiptAsync(string recipientEmail, VoucherRedemptionResult redemptionResult, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!redemptionResult.IsSuccess)
        {
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            throw new ArgumentException("Recipient email is required.", nameof(recipientEmail));
        }

        _sentReceiptAudit.Add($"{recipientEmail}:{redemptionResult.RedeemedAmount:0.00}:{redemptionResult.Message}");
        return Task.CompletedTask;
    }
}
