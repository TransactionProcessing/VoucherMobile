namespace VoucherMobile.App.Pages.AppHome;

[QueryProperty(nameof(UserName), "username")]
public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    public String UserName
    {
        set
        {
            String displayName = Uri.UnescapeDataString(value ?? String.Empty);
            this.WelcomeLabel.Text = String.IsNullOrWhiteSpace(displayName)
                ? "Welcome"
                : $"Welcome, {displayName}";
        }
    }

    private async void OnSignOutClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///login");
    }
}
