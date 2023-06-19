using EducationMauiApp.UIElements;
using EducationMauiApp.ViewModels;
using GraphLib.Models;

namespace EducationMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    private void GraphLayout_Tapped(object sender, TappedEventArgs e)
    {
        var node = new Node((Point)e.GetPosition((View)sender));
        App.GraphLayoutViewModel.AddNodeCommand.Execute(node);
    }

    private void GraphElement_Primary_Tapped(object sender, TappedEventArgs e)
    {
        var element = ((GraphViewElement)sender).GraphElement;
        if (element is null) return;
        else if (element is Edge)
        {
            App.GraphLayoutViewModel.AddNodeCommand.Execute(new Node((Point)e.GetPosition((View)(sender as GraphViewElement).Parent)));
            return;
        }
        App.GraphLayoutViewModel.AddEdgeCommand.Execute(element as Node);
    }

    private void GraphElement_Secondary_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not GraphViewElement) return;
        var element = sender as GraphViewElement;
        if (element.GraphElement is not Node) return;
        App.GraphLayoutViewModel.WorkingNode = element;
    }
}