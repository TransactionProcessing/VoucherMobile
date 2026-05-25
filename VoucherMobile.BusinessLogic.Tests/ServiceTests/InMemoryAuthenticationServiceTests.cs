using VoucherMobile.BusinessLogic.Models;
using VoucherMobile.BusinessLogic.Services;

namespace VoucherMobile.BusinessLogic.Tests.ServiceTests;

public class InMemoryAuthenticationServiceTests
{
    private readonly InMemoryAuthenticationService service = new();

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ReturnsSuccess()
    {
        LoginResult result = await this.service.AuthenticateAsync("demo", "Password123!", CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Demo User", result.DisplayName);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public async Task AuthenticateAsync_WithInvalidCredentials_ReturnsFailure()
    {
        LoginResult result = await this.service.AuthenticateAsync("demo", "wrong-password", CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid username or password.", result.ErrorMessage);
    }
}
