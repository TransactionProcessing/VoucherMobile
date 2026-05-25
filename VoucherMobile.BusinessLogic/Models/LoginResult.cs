namespace VoucherMobile.BusinessLogic.Models;

public sealed record LoginResult(Boolean IsSuccess, String? DisplayName = null, String? ErrorMessage = null);
