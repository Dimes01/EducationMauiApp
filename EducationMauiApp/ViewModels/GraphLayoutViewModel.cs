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

        #region Свойства

        public ObservableCollection<GraphViewElement> GraphElements { get; private set; } = new ObservableCollection<GraphViewElement>();


        private GraphViewElement workingNode;
        public GraphViewElement WorkingNode
        {
            get => workingNode;
            set
            {
                if (workingNode == value) return;
                workingNode = value;
                OnPropertyChanged(nameof(WorkingNode));
            }
        }

        #endregion

        #region Команды

        private ICommand addNodeCommand;
		public ICommand AddNodeCommand => addNodeCommand ??= new Command(f =>
		{
			if (f is not Point) return;
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
        });


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
                Margin = new Thickness(minOfCoordinates.X, minOfCoordinates.Y, 0, 0),
            };
            GraphElements.Add(viewEdge);
            tempEdge = new();
        });


		private ICommand setStartEdgeCommand;
		public ICommand SetStartEdgeCommand => setStartEdgeCommand ??= new Command(f =>
		{
            if (WorkingNode is null) return;
            tempEdge.Nodes[0] = (Node)WorkingNode.GraphElement;
            if (tempEdge.Nodes[0] != null && tempEdge.Nodes[1] != null) AddEdgeCommand.Execute(null);
        });


		private ICommand setEndEdgeCommand;
		public ICommand SetEndEdgeCommand => setEndEdgeCommand ??= new Command(f =>
		{
            if (WorkingNode is null) return;
            tempEdge.Nodes[1] = (Node)WorkingNode.GraphElement;
            if (tempEdge.Nodes[0] != null && tempEdge.Nodes[1] != null) AddEdgeCommand.Execute(null);
        });


		private ICommand removeNodeCommand;
		public ICommand RemoveNodeCommand => removeNodeCommand ??= new Command(f =>
		{
            List<GraphViewElement> removeEdges = new();
            for (int i = 0; i < GraphElements.Count; ++i)
            {
                if (GraphElements[i].GraphElement is not Edge) continue;
                var edge = GraphElements[i].GraphElement as Edge;
                if (edge.Nodes[0] == WorkingNode.GraphElement || edge.Nodes[1] == WorkingNode.GraphElement)
                    removeEdges.Add(GraphElements[i]);
            }
            for (int i = 0; i < removeEdges.Count; ++i)
            {
                GraphElements.Remove(removeEdges[i]);
                removeEdges[i].GraphElement.Remove();
            }
            for (int i = 0; i < GraphElements.Count; ++i)
            {
                if (GraphElements[i].GraphElement != WorkingNode.GraphElement) continue;
                GraphElements.RemoveAt(i);
                break;
            }
            //GraphElements.Remove(WorkingNode);
            WorkingNode.GraphElement.Remove();
            WorkingNode = null;
        });


        private ICommand scaleUpCommand;
        public ICommand ScaleUpCommand => scaleUpCommand ??= new Command(layout =>
        {
            if (layout is not AbsoluteLayout) return;
            var panel = layout as AbsoluteLayout;
            panel.ScaleTo(panel.Scale + 0.1);
        });


        private ICommand scaleDownCommand;
        public ICommand ScaleDownCommand => scaleDownCommand ??= new Command(layout =>
        {
            if (layout is not AbsoluteLayout) return;
            var panel = layout as AbsoluteLayout;
            panel.ScaleTo(panel.Scale - 0.1);
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
                        StartPoint = start,
                        Segments = new PathSegmentCollection { new LineSegment(end) }
                    }
                });
        }

        #endregion
    }
}
