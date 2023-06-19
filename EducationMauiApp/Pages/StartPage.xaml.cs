namespace EducationMauiApp.Pages;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

    private async void NewProject_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        //await Navigation.PushAsync(new AppShell());
    }

    private void OpenProject_Clicked(object sender, EventArgs e)
    {

    }
}