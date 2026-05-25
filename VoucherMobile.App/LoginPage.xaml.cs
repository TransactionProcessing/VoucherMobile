namespace VoucherMobile.App;

public partial class LoginPage : ContentPage
{
    private AppState State => ((App)Application.Current!).State;

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = State;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (await State.Login.LoginAsync())
        {
            await Shell.Current!.GoToAsync(nameof(VoucherLookupPage));
        }
    }
}
