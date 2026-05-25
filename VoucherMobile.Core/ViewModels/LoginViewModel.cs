using VoucherMobile.Core.Interfaces;

namespace VoucherMobile.Core.ViewModels;

public sealed class LoginViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private bool _isAuthenticated;
    private string _statusMessage = string.Empty;

    public LoginViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        private set => SetProperty(ref _isAuthenticated, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public async Task<bool> LoginAsync(CancellationToken cancellationToken = default)
    {
        IsAuthenticated = await _authenticationService.AuthenticateAsync(Username, Password, cancellationToken);
        StatusMessage = IsAuthenticated ? "Login successful." : "Invalid credentials.";
        return IsAuthenticated;
    }
}
