using EducationMauiApp.UIElements;
using GraphLib.Models;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EducationMauiApp.ViewModels
{
	internal class GraphLayoutViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


		private const double radiusNode = 10;
		private Edge tempEdge = new();
		public ObservableCollection<GraphViewElement> GraphElements { get; private set; } = new ObservableCollection<GraphViewElement>();



        #region Команды

        private ICommand addNodeCommand;
		public ICommand AddNodeCommand => addNodeCommand ??= new Command(f =>
		{
			if (f is not Point || !App.ConditionsViewModel.IsEditingMode) return;
			var position = (Point)f;
            var node = new Node(position);
            var viewNode = new GraphViewElement
            {
                Geometry = CreatedDefaultEllipse(new Point(radiusNode, radiusNode)),
                GraphElement = node,
                ZIndex = 50,
                Margin = new Thickness(position.X - radiusNode, position.Y - radiusNode, 0, 0)
            };
            GraphElements.Add(viewNode);
        }, f => App.ConditionsViewModel.IsEditingMode);


		private ICommand addEdgeCommand;
		public ICommand AddEdgeCommand => addEdgeCommand ??= new Command(f =>
		{
            if (f is Node)
            {
                var node = (Node)f;
                if (tempEdge.Nodes[0] == null) tempEdge.Nodes[0] = node;
                else if (tempEdge.Nodes[1] == null && node != tempEdge.Nodes[0]) tempEdge.Nodes[1] = node;
            }
            else if (f is not null) return;

            if (tempEdge.Nodes[0] == null || tempEdge.Nodes[1] == null) return;

            Point minOfCoordinates = new();
            if (tempEdge.Nodes[0].Position.X < tempEdge.Nodes[1].Position.X) minOfCoordinates.X = tempEdge.Nodes[0].Position.X;
            else minOfCoordinates.X = tempEdge.Nodes[1].Position.X;
            if (tempEdge.Nodes[0].Position.Y < tempEdge.Nodes[1].Position.Y) minOfCoordinates.Y = tempEdge.Nodes[0].Position.Y;
            else minOfCoordinates.Y = tempEdge.Nodes[1].Position.Y;

            var pointStart = new Point(tempEdge.Nodes[0].Position.X - minOfCoordinates.X, tempEdge.Nodes[0].Position.Y - minOfCoordinates.Y);
            var pointEnd = new Point(tempEdge.Nodes[1].Position.X - minOfCoordinates.X, tempEdge.Nodes[1].Position.Y - minOfCoordinates.Y);
            var viewEdge = new GraphViewElement
            {
                Geometry = CreatedDefaultLine(pointStart, pointEnd),
                GraphElement = tempEdge,
                ZIndex = 45,
                Margin = new Thickness(minOfCoordinates.X, minOfCoordinates.Y, 0, 0)
            };

            GraphElements.Add(viewEdge);
            tempEdge = new();
        });


		private ICommand setStartEdgeCommand;
		public ICommand SetStartEdgeCommand => setStartEdgeCommand ??= new Command(f =>
		{
            if (f is not Node) return;
            tempEdge.Nodes[0] = (Node)f;
            if (tempEdge.Nodes[0] != null && tempEdge.Nodes[1] != null) AddEdgeCommand.Execute(null);
        });


		private ICommand setEndEdgeCommand;
		public ICommand SetEndEdgeCommand => setEndEdgeCommand ??= new Command(f =>
		{
            if (f is not Node) return;
            tempEdge.Nodes[1] = (Node)f;
            if (tempEdge.Nodes[0] != null && tempEdge.Nodes[1] != null) AddEdgeCommand.Execute(null);
        });


		private ICommand removeNodeCommand;
		public ICommand RemoveNodeCommand => removeNodeCommand ??= new Command(f =>
		{
            
		});


		#endregion
		#region Вспомогательные методы

		private Geometry CreatedDefaultEllipse(Point position) => new EllipseGeometry { Center = position, RadiusX = radiusNode, RadiusY = radiusNode };
        private Geometry CreatedDefaultLine(Point start, Point end)
        {
            var length = start.Distance(end);
            return new PathGeometry(
                new PathFigureCollection
                {
                    new PathFigure
                    {
                        StartPoint = new Point(0, 0),
                        Segments = new PathSegmentCollection { new LineSegment(new Point(length, 0)) }
                    }
                });
        }
        private double AngleBetweenPoints(Point first, Point second)
        {
            var vector1 = new Vector2((float)first.X, (float)first.Y);
            var vector2 = new Vector2((float)second.X, (float)second.Y);
            var dot = Vector2.Dot(vector1, vector2);
            return Math.Acos(dot) * Math.PI / 180;
        }
        #endregion
    }
}
