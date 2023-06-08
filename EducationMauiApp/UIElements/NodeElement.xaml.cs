using EducationMauiApp.Interfaces;

namespace EducationMauiApp.UIElements;

public partial class NodeElement : ContentView, IGraphViewElement
{
	public NodeElement()
	{
		InitializeComponent();
	}


	public static readonly BindableProperty FillProperty = BindableProperty.Create(nameof(Fill), typeof(Brush), typeof(NodeElement), 
		App.Current.Resources.MergedDictionaries.ElementAt(0)["FillNodeBrush"]);
	public Brush Fill
	{
		get => (Brush)GetValue(FillProperty);
		set => SetValue(FillProperty, value);
	}


	public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(NodeElement), 
		App.Current.Resources.MergedDictionaries.ElementAt(0)["StrokeNodeBrush"]);
	public Brush Stroke
	{
		get => (Brush)GetValue(StrokeProperty);
		set => SetValue(StrokeProperty, value);
	}


	public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(NodeElement), 2.0);
	public double StrokeThickness
	{
		get => (double)GetValue(StrokeThicknessProperty);
		set => SetValue(StrokeThicknessProperty, value);
	}


	public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(Point), typeof(NodeElement));
	public Point Position
	{
		get => (Point)GetValue(PositionProperty);
		set => SetValue(PositionProperty, value);
	}
}