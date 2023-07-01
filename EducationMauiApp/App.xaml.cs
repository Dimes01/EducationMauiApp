using EducationMauiApp.ViewModels;

namespace EducationMauiApp;

public partial class App : Application
{
    internal static GraphLayoutViewModel GraphLayoutViewModel { get; private set; }
    public App()
    {
        InitializeComponent();

        // !!! В данном случае, присваивание значения - необходимость, обусловленная тем, что при создании приложения на Android
        //	при инициализации GraphLayoutViewModel выбрасывается ошибка
        //		System.TypeInitializationException: 'The type initializer for 'EducationMauiApp.App' threw an exception.',
        //	связанная со статическим конструктором.
        GraphLayoutViewModel = (GraphLayoutViewModel)Current.Resources[nameof(GraphLayoutViewModel)];

        MainPage = new AppShell();
//#if WINDOWS
//        MainPage = new WindowsMainPage();
//#elif ANDROID
//        MainPage = new AndroidMainPage();
//#endif
    }
}
