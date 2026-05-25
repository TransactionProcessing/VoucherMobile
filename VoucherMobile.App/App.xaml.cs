namespace VoucherMobile.App;

public partial class App : Application
{
	public AppState State { get; }

	public App()
	{
		InitializeComponent();
		State = new AppState();

		MainPage = new AppShell();
	}
}
