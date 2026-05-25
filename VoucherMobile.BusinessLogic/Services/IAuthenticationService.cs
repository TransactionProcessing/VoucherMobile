using VoucherMobile.BusinessLogic.Models;

namespace VoucherMobile.BusinessLogic.Services;

public interface IAuthenticationService
{
    Task<LoginResult> AuthenticateAsync(String userName, String password, CancellationToken cancellationToken);
}
