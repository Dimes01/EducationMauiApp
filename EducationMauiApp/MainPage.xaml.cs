using EducationMauiApp.UIElements;

namespace EducationMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    private void GraphLayout_Tapped(object sender, TappedEventArgs e)
    {
        var position = (Point)e.GetPosition((View)sender);
        App.GraphLayoutViewModel.AddGraphElementCommand.Execute(position);
    }

    private void GraphElement_Tapped(object sender, TappedEventArgs e)
    {
        //App.GraphLayoutViewModel.SelectedNode.IsSelected = false;
        //App.GraphLayoutViewModel.SelectedNode = (NodeElement)sender;
        //App.GraphLayoutViewModel.SelectedNode.IsSelected = true;
    }
}