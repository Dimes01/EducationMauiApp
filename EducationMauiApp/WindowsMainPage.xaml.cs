using EducationMauiApp.UIElements;
using EducationMauiApp.ViewModels;
using GraphLib.Models;

namespace EducationMauiApp;

public partial class WindowsMainPage : ContentPage
{
	public WindowsMainPage()
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
        var viewElement = (GraphViewElement)sender;
        var graphElement = viewElement.GraphElement;
        if (graphElement is null) return;
        else if (graphElement is Edge)
        {
            App.GraphLayoutViewModel.AddNodeCommand.Execute(new Node((Point)e.GetPosition((View)(sender as GraphViewElement).Parent)));
            return;
        }
        App.GraphLayoutViewModel.AddEdgeCommand.Execute(graphElement as Node);
    }

    private void GraphElement_Secondary_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is not GraphViewElement) return;
        var viewElement = sender as GraphViewElement;
        if (viewElement.GraphElement is not Node) return;
        App.GraphLayoutViewModel.WorkingNode = viewElement;
    }
}