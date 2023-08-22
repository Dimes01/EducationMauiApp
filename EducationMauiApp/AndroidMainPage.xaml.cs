using EducationMauiApp.UIElements;
using EducationMauiApp.ViewModels;
using GraphLib.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    private void GraphPanel_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        var panel = sender as AbsoluteLayout;
        if (e.StatusType == GestureStatus.Running)
        {
            panel.TranslationX += e.TotalX;
            panel.TranslationY += e.TotalY;
        }
    }
}