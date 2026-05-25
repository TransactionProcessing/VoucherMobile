using VoucherMobile.Core.Interfaces;

namespace VoucherMobile.Core.Services;

public sealed class InMemoryAuthenticationService : IAuthenticationService
{
    private readonly IDictionary<string, string> _credentialStore;

    public InMemoryAuthenticationService(IDictionary<string, string>? credentialStore = null)
    {
        _credentialStore = credentialStore ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["provider.user"] = "P@ssw0rd!"
        };
    }

    public Task<bool> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(_credentialStore.TryGetValue(username.Trim(), out var storedPassword) && storedPassword == password);
    }
}
