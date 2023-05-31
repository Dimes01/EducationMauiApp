using EducationMauiApp.ViewModels;

namespace EducationMauiApp;

public partial class App : Application
{
	public static GraphLayoutViewModel GraphLayoutViewModel { get; } = (GraphLayoutViewModel)Current.Resources[nameof(GraphLayoutViewModel)];
    public App()
	{
		InitializeComponent();
		//GraphLayoutViewModel = (GraphLayoutViewModel)Current.Resources[nameof(GraphLayoutViewModel)];
        MainPage = new AppShell();
	}
}
