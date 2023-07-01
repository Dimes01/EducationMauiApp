using EducationMauiApp.UIElements;
using EducationMauiApp.ViewModels;
using GraphLib.Models;

namespace EducationMauiApp;

public partial class AndroidMainPage : ContentPage
{
	public AndroidMainPage()
	{
		InitializeComponent();
	}

    private void GraphLayout_Tapped(object sender, TappedEventArgs e)
    {
        if (App.GraphLayoutViewModel.VisibilityMenuGraphElement)
        {
            App.GraphLayoutViewModel.CloseMenuGraphElement();
            return;
        }

        var node = new Node((Point)e.GetPosition((View)sender));
        App.GraphLayoutViewModel.AddNodeCommand.Execute(node);
    }

    private void GraphElement_Tapped(object sender, TappedEventArgs e)
    {
        var viewElement = (GraphViewElement)sender;
        var graphElement = viewElement.GraphElement;
        if (graphElement is null) return;
        else if (graphElement is Node)
        {
            App.GraphLayoutViewModel.WorkingNode = viewElement;
            App.GraphLayoutViewModel.CloseMenuGraphElement();
            App.GraphLayoutViewModel.OpenMenuGraphElement((graphElement as Node).Position);
        }

        if (graphElement is Node) return;
        var pos = e.GetPosition(viewElement.Parent as View);
        if (pos is null) return;
        App.GraphLayoutViewModel.AddNodeCommand.Execute(new Node((Point)pos));
    }
}