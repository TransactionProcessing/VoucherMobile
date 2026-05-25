using VoucherMobile.Core.Interfaces;
using VoucherMobile.Core.Models;

namespace VoucherMobile.Core.ViewModels;

public sealed class VoucherRedemptionViewModel : ViewModelBase
{
    private readonly IVoucherService _voucherService;
    private readonly IReceiptService _receiptService;

    private string _voucherCode = string.Empty;
    private string _merchantId = string.Empty;
    private string _transactionReference = string.Empty;
    private decimal _redemptionAmount;
    private Voucher? _currentVoucher;
    private string _statusMessage = string.Empty;

    public VoucherRedemptionViewModel(IVoucherService voucherService, IReceiptService receiptService)
    {
        _voucherService = voucherService;
        _receiptService = receiptService;
    }

    public string VoucherCode
    {
        get => _voucherCode;
        set => SetProperty(ref _voucherCode, value);
    }

    public string MerchantId
    {
        get => _merchantId;
        set => SetProperty(ref _merchantId, value);
    }

    public string TransactionReference
    {
        get => _transactionReference;
        set => SetProperty(ref _transactionReference, value);
    }

    public decimal RedemptionAmount
    {
        get => _redemptionAmount;
        set => SetProperty(ref _redemptionAmount, value);
    }

    public Voucher? CurrentVoucher
    {
        get => _currentVoucher;
        private set => SetProperty(ref _currentVoucher, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public async Task<bool> ScanVoucherAsync(CancellationToken cancellationToken = default)
    {
        CurrentVoucher = await _voucherService.GetVoucherAsync(VoucherCode, cancellationToken);

        if (CurrentVoucher is null)
        {
            StatusMessage = "Voucher not found.";
            return false;
        }

        StatusMessage = CurrentVoucher.IsExpired()
            ? "Voucher has expired."
            : $"Voucher loaded. Remaining balance: {CurrentVoucher.RemainingBalance:0.00}";

        return !CurrentVoucher.IsExpired();
    }

    public async Task<VoucherRedemptionResult> RedeemAsync(CancellationToken cancellationToken = default)
    {
        var transaction = new TransactionDetails
        {
            MerchantId = MerchantId,
            TransactionReference = TransactionReference,
            RedemptionAmount = RedemptionAmount,
            TransactionTimeUtc = DateTime.UtcNow
        };

        var result = await _voucherService.RedeemAsync(VoucherCode, transaction, cancellationToken);
        StatusMessage = result.Message;

        if (result.IsSuccess)
        {
            CurrentVoucher = result.Voucher;
            if (CurrentVoucher is not null)
            {
                await _receiptService.SendReceiptAsync(CurrentVoucher.CustomerEmail, result, cancellationToken);
            }
        }

        return result;
    }
}
