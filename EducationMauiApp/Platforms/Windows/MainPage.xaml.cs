using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace EducationMauiApp;

public partial class MainPage : ContentPage
{
    partial void ChangedHandler(object sender, EventArgs e)
    {
        AbsoluteLayout view = sender as AbsoluteLayout;
        Canvas canv = view.Handler.PlatformView as ;
        canv.PointerPressed += CustomizeGraphPanel_PointerPressed;
    }

    partial void ChangingHandler(object sender, HandlerChangingEventArgs e)
    {
        if (e.OldHandler == null) return;
        (e.OldHandler.PlatformView as Canvas).PointerPressed -= CustomizeGraphPanel_PointerPressed;
    }

    private void CustomizeGraphPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        Canvas panel = (sender as AbsoluteLayout).Handler.PlatformView as Canvas;
        App.GraphLayoutViewModel.AddGraphElementCommand.Execute(e.GetCurrentPoint(panel));
    }


}