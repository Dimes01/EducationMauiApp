namespace EducationMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    Point position = (Point)e.GetPosition((Layout)sender);
    //    App.GraphLayoutViewModel.AddGraphElementCommand.Execute(position);
    //}

    partial void ChangedHandler(object sender, EventArgs e);
    partial void ChangingHandler(object sender, HandlerChangingEventArgs e);

    private void GraphLayout_HandlerChanged(object sender, EventArgs e) => ChangedHandler(sender, e);
    private void GraphLayout_HandlerChanging(object sender, HandlerChangingEventArgs e) => ChangingHandler(sender, e);
}

