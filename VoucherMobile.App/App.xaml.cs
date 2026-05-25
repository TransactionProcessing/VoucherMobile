using VoucherMobile.App.Resources.Styles;

namespace VoucherMobile.App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		this.ApplyTheme(this.RequestedTheme);
		this.RequestedThemeChanged += this.OnRequestedThemeChanged;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}

	private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e) => this.ApplyTheme(e.RequestedTheme);

	private void ApplyTheme(AppTheme requestedTheme)
	{
		ICollection<ResourceDictionary> mergedDictionaries = this.Resources.MergedDictionaries;
		ResourceDictionary selectedTheme = requestedTheme == AppTheme.Dark
			? new DarkTheme()
			: new LightTheme();

		ResourceDictionary? existingTheme = mergedDictionaries
			.FirstOrDefault(d => d is LightTheme or DarkTheme);

		if (existingTheme == null)
		{
			mergedDictionaries.Add(selectedTheme);
			return;
		}

		if (mergedDictionaries is IList<ResourceDictionary> dictionaryList)
		{
			Int32 index = dictionaryList.IndexOf(existingTheme);
			dictionaryList.RemoveAt(index);
			dictionaryList.Insert(index, selectedTheme);
			return;
		}

		List<ResourceDictionary> dictionaries = mergedDictionaries.ToList();
		Int32 replacementIndex = dictionaries.IndexOf(existingTheme);
		dictionaries[replacementIndex] = selectedTheme;

		mergedDictionaries.Clear();
		foreach (ResourceDictionary dictionary in dictionaries)
		{
			mergedDictionaries.Add(dictionary);
		}
	}
}
