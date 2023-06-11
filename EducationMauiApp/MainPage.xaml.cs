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
        var position = (Point)e.GetPosition((View)sender);
        if (App.GraphLayoutViewModel.AddNodeCommand.CanExecute(null))
            App.GraphLayoutViewModel.AddNodeCommand.Execute(position);
    }

    private void GraphElement_Tapped(object sender, TappedEventArgs e)
    {
        var element = ((GraphViewElement)sender).GraphElement as Node;
        if (element == null) return;
        if (App.GraphLayoutViewModel.AddEdgeCommand.CanExecute(null))
            App.GraphLayoutViewModel.AddEdgeCommand.Execute(element);
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radiobutton = (RadioButton)sender;
        App.ConditionsViewModel.IsEditingMode = radiobutton.IsChecked;
    }

    private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        //var layout = (AbsoluteLayout)sender;
        //layout.ScaleTo(e.Scale);
    }
}