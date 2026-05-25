using VoucherMobile.BusinessLogic.Models;

namespace VoucherMobile.BusinessLogic.Services;

public sealed class InMemoryAuthenticationService : IAuthenticationService
{
    private static readonly Dictionary<String, (String Password, String DisplayName)> Users =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["demo"] = ("Password123!", "Demo User")
        };

    public Task<LoginResult> AuthenticateAsync(String userName, String password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        String normalizedUserName = userName.Trim();

        if (Users.TryGetValue(normalizedUserName, out (String Password, String DisplayName) user) &&
            String.Equals(user.Password, password, StringComparison.Ordinal))
        {
            return Task.FromResult(new LoginResult(true, user.DisplayName));
        }

        return Task.FromResult(new LoginResult(false, ErrorMessage: "Invalid username or password."));
    }
}
