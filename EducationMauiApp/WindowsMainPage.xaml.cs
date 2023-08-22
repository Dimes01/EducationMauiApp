using EducationMauiApp.UIElements;
using GraphLib.Models;

namespace EducationMauiApp;

public partial class WindowsMainPage : ContentPage
{
    private double panX, panY;
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

    private void GraphPanel_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        var panel = sender as AbsoluteLayout;
        if (e.StatusType == GestureStatus.Running)
        {
            panel.TranslationX = Math.Clamp(panX + e.TotalX, -panel.Width, panel.Height);
            panel.TranslationY = Math.Clamp(panY + e.TotalY, -panel.Width, panel.Height);
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            panX = panel.TranslationX;
            panY = panel.TranslationY;
        }
    }
}