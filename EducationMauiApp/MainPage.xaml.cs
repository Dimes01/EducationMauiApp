namespace EducationMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Point position = (Point)e.GetPosition((Layout)sender);
        App.GraphLayoutViewModel.AddGraphElement.Execute(position);
    }
}

