namespace VoucherMobile.Core.Interfaces;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
}
