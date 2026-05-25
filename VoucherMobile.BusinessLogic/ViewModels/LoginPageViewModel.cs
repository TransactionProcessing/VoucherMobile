using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VoucherMobile.BusinessLogic.Models;
using VoucherMobile.BusinessLogic.Services;

namespace VoucherMobile.BusinessLogic.ViewModels;

public partial class LoginPageViewModel : ObservableObject
{
    private readonly IAuthenticationService authenticationService;

    [ObservableProperty]
    private String userName = String.Empty;

    [ObservableProperty]
    private String password = String.Empty;

    [ObservableProperty]
    private String statusMessage = String.Empty;

    [ObservableProperty]
    private Boolean isBusy;

    [ObservableProperty]
    private String? authenticatedUserName;

    public LoginPageViewModel(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    public Boolean HasStatusMessage => String.IsNullOrWhiteSpace(this.StatusMessage) == false;

    public Boolean IsAuthenticated => String.IsNullOrWhiteSpace(this.AuthenticatedUserName) == false;

    [RelayCommand(CanExecute = nameof(CanLogon))]
    private async Task LogonAsync()
    {
        if (String.IsNullOrWhiteSpace(this.UserName) || String.IsNullOrWhiteSpace(this.Password))
        {
            this.AuthenticatedUserName = null;
            this.StatusMessage = "Enter a username and password.";
            return;
        }

        this.StatusMessage = String.Empty;
        this.AuthenticatedUserName = null;
        this.IsBusy = true;

        try
        {
            LoginResult result = await this.authenticationService.AuthenticateAsync(this.UserName, this.Password, CancellationToken.None);

            if (result.IsSuccess == false)
            {
                this.Password = String.Empty;
                this.StatusMessage = result.ErrorMessage ?? "Login failed.";
                return;
            }

            this.AuthenticatedUserName = result.DisplayName ?? this.UserName.Trim();
        }
        finally
        {
            this.IsBusy = false;
        }
    }

    public void ResetForDisplay()
    {
        this.Password = String.Empty;
        this.StatusMessage = String.Empty;
        this.AuthenticatedUserName = null;
    }

    private Boolean CanLogon() => this.IsBusy == false;

    partial void OnStatusMessageChanged(String value) => this.OnPropertyChanged(nameof(HasStatusMessage));

    partial void OnAuthenticatedUserNameChanged(String? value) => this.OnPropertyChanged(nameof(IsAuthenticated));

    partial void OnIsBusyChanged(Boolean value) => this.LogonCommand.NotifyCanExecuteChanged();
}
