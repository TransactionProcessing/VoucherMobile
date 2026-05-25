using VoucherMobile.BusinessLogic.Models;
using VoucherMobile.BusinessLogic.Services;
using VoucherMobile.BusinessLogic.ViewModels;

namespace VoucherMobile.BusinessLogic.Tests.ViewModelTests;

public class LoginPageViewModelTests
{
    [Fact]
    public async Task LogonCommand_WithBlankCredentials_ShowsValidationMessage()
    {
        LoginPageViewModel viewModel = new(new StubAuthenticationService(new LoginResult(true, "Ignored")));

        await viewModel.LogonCommand.ExecuteAsync(null);

        Assert.Equal("Enter a username and password.", viewModel.StatusMessage);
        Assert.False(viewModel.IsAuthenticated);
    }

    [Fact]
    public async Task LogonCommand_WithRejectedCredentials_ShowsFailure()
    {
        LoginPageViewModel viewModel = new(new StubAuthenticationService(new LoginResult(false, ErrorMessage: "Invalid username or password.")))
        {
            UserName = "demo",
            Password = "bad-password"
        };

        await viewModel.LogonCommand.ExecuteAsync(null);

        Assert.Equal("Invalid username or password.", viewModel.StatusMessage);
        Assert.Equal(String.Empty, viewModel.Password);
        Assert.False(viewModel.IsAuthenticated);
    }

    [Fact]
    public async Task LogonCommand_WithAcceptedCredentials_SetsAuthenticatedUser()
    {
        LoginPageViewModel viewModel = new(new StubAuthenticationService(new LoginResult(true, "Demo User")))
        {
            UserName = "demo",
            Password = "Password123!"
        };

        await viewModel.LogonCommand.ExecuteAsync(null);

        Assert.True(viewModel.IsAuthenticated);
        Assert.Equal("Demo User", viewModel.AuthenticatedUserName);
        Assert.Equal(String.Empty, viewModel.StatusMessage);
    }

    [Fact]
    public void ResetForDisplay_ClearsSensitiveState()
    {
        LoginPageViewModel viewModel = new(new StubAuthenticationService(new LoginResult(true, "Demo User")))
        {
            UserName = "demo",
            Password = "Password123!",
            AuthenticatedUserName = "Demo User",
            StatusMessage = "Should clear"
        };

        viewModel.ResetForDisplay();

        Assert.Equal(String.Empty, viewModel.Password);
        Assert.Equal(String.Empty, viewModel.StatusMessage);
        Assert.Null(viewModel.AuthenticatedUserName);
        Assert.False(viewModel.IsAuthenticated);
    }

    private sealed class StubAuthenticationService(LoginResult result) : IAuthenticationService
    {
        public Task<LoginResult> AuthenticateAsync(String userName, String password, CancellationToken cancellationToken) =>
            Task.FromResult(result);
    }
}
