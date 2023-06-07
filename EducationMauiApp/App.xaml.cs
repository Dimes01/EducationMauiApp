using EducationMauiApp.ViewModels;

namespace EducationMauiApp;

public partial class App : Application
{
	internal static GraphLayoutViewModel GraphLayoutViewModel { get; private set; }
    internal static ConditionsViewModel ConditionsViewModel { get; private set; }
    public App()
	{
		InitializeComponent();

        // !!! В данном случае, присваивание значения - необходимость, обусловленная тем, что при создании приложения на Android
        //	при инициализации GraphLayoutViewModel выбрасывается ошибка
        //		System.TypeInitializationException: 'The type initializer for 'EducationMauiApp.App' threw an exception.',
        //	связанная со статическим конструктором.
        GraphLayoutViewModel = (GraphLayoutViewModel)Current.Resources[nameof(GraphLayoutViewModel)];
        ConditionsViewModel = (ConditionsViewModel)Current.Resources[nameof(ConditionsViewModel)];

        MainPage = new AppShell();
    }
}
