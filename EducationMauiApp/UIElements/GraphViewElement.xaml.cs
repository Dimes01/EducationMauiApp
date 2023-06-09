using GraphLib.Models;
using Microsoft.Maui.Controls.Shapes;

namespace EducationMauiApp.UIElements;

public partial class GraphViewElement : ContentView
{
	public GraphViewElement()
	{
		InitializeComponent();
	}


    public static readonly BindableProperty FillProperty = BindableProperty.Create(nameof(Fill), typeof(Brush), typeof(GraphViewElement),
        App.Current.Resources.MergedDictionaries.ElementAt(0)["FillNodeBrush"]);
    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }


    public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(GraphViewElement),
        App.Current.Resources.MergedDictionaries.ElementAt(0)["StrokeNodeBrush"]);
    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }


    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(GraphViewElement), 2.0);
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }


    public static readonly BindableProperty GeometryProperty = BindableProperty.Create(nameof(Geometry), typeof(Geometry), typeof(GraphViewElement));
    public Geometry Geometry
    {
        get => (Geometry)GetValue(GeometryProperty);
        set => SetValue(GeometryProperty, value);
    }


    public static readonly BindableProperty GraphElementProperty = BindableProperty.Create(nameof(GraphElement), typeof(GraphElement), typeof(GraphViewElement));
    public GraphElement GraphElement
    {
        get => (GraphElement)GetValue(GraphElementProperty);
        set => SetValue(GraphElementProperty, value);
    }
}