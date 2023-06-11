using EducationMauiApp.UIElements;
using GraphLib.Models;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
			if (f is not Node) return;
			var node = (Node)f;
			if (tempEdge.Nodes[0] == null)
			{
				tempEdge.Nodes[0] = node;
			}
			else if (tempEdge.Nodes[1] == null && node != tempEdge.Nodes[0])
			{
				tempEdge.Nodes[1] = node;
				Point minCoor = new();
				if (tempEdge.Nodes[0].Position.X < tempEdge.Nodes[1].Position.X) minCoor.X = tempEdge.Nodes[0].Position.X;
				else minCoor.X = tempEdge.Nodes[1].Position.X;
                if (tempEdge.Nodes[0].Position.Y < tempEdge.Nodes[1].Position.Y) minCoor.Y = tempEdge.Nodes[0].Position.Y;
                else minCoor.Y = tempEdge.Nodes[1].Position.Y;

				var pointStart = new Point(tempEdge.Nodes[0].Position.X - minCoor.X, tempEdge.Nodes[0].Position.Y - minCoor.Y);
				var pointEnd = new Point(tempEdge.Nodes[1].Position.X - minCoor.X, tempEdge.Nodes[1].Position.Y - minCoor.Y);
                var viewEdge = new GraphViewElement
                {
                    Geometry = CreatedDefaultLine(pointStart, pointEnd),
                    GraphElement = tempEdge,
                    ZIndex = 45,
                    Margin = new Thickness(minCoor.X, minCoor.Y, 0, 0)
                };
                GraphElements.Add(viewEdge);
				tempEdge = new();
			}
		});


		private ICommand setStartEdgeCommand;
		public ICommand SetStartEdgeCommand => setStartEdgeCommand ??= new Command(f =>
		{

		});


		private ICommand setEndEdgeCommand;
		public ICommand SetEndEdgeCommand => setEndEdgeCommand ??= new Command(f =>
		{

		});


		private ICommand removeNodeCommand;
		public ICommand RemoveNodeCommand => removeNodeCommand ??= new Command(f =>
		{

		});


		#endregion
		#region Вспомогательные методы

		private Geometry CreatedDefaultEllipse(Point position) => new EllipseGeometry { Center = position, RadiusX = radiusNode, RadiusY = radiusNode };
		private Geometry CreatedDefaultLine(Point start, Point end) 
			=> new PathGeometry(
				new PathFigureCollection
				{
                    new PathFigure
                    {
                        StartPoint = start,
                        Segments = new PathSegmentCollection { new LineSegment(end) }
                    }
                });
        #endregion
    }
}
