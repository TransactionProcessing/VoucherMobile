using VoucherMobile.Core.Services;
using VoucherMobile.Core.ViewModels;

namespace VoucherMobile.Core.Tests.ViewModels;

public sealed class LoginViewModelTests
{
    [Fact]
    public async Task LoginAsync_Succeeds_ForAuthorisedProvider()
    {
        var vm = new LoginViewModel(new InMemoryAuthenticationService());
        vm.Username = "provider.user";
        vm.Password = "P@ssw0rd!";

        var success = await vm.LoginAsync();

        Assert.True(success);
        Assert.True(vm.IsAuthenticated);
        Assert.Equal("Login successful.", vm.StatusMessage);
    }
}
