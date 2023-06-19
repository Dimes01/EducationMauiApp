using EducationMauiApp.Pages;

namespace EducationMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		OnStartPage();
	}

	public async void OnStartPage() => await Navigation.PushAsync(new StartPage());
}
