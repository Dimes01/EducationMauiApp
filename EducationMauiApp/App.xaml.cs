using EducationMauiApp.ViewModels;

namespace EducationMauiApp;

public partial class App : Application
{
	public static GraphLayoutViewModel GraphLayoutViewModel { get; private set; }
    public App()
	{
		InitializeComponent();
		GraphLayoutViewModel = (GraphLayoutViewModel)Current.Resources[nameof(GraphLayoutViewModel)];
		MainPage = new AppShell();
	}
}
