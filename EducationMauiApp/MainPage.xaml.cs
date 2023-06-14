﻿using EducationMauiApp.UIElements;
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

    private void GraphElement_Primary_Tapped(object sender, TappedEventArgs e)
    {
        var element = ((GraphViewElement)sender).GraphElement;
        if (element is null) return;
        else if (element is Edge)
        {
            App.GraphLayoutViewModel.AddNodeCommand.Execute((Point)e.GetPosition((View)(sender as GraphViewElement).Parent));
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

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        if (sender is not MenuFlyoutItem) return;
        var menu = (MenuFlyoutItem)sender;
        if (menu.Text == "Начало ребра") App.GraphLayoutViewModel.SetStartEdgeCommand.Execute(null);
        else if (menu.Text == "Конец ребра") App.GraphLayoutViewModel.SetEndEdgeCommand.Execute(null);
        else if (menu.Text == "Удалить узел") App.GraphLayoutViewModel.RemoveNodeCommand.Execute(null);
        else DisplayAlert("Контекстное меню", "Отсутствие команды для пункта", "Закрыть");
    }

}