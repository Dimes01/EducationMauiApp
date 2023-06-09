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


		private ICommand addNodeCommand;
		public ICommand AddNodeCommand => addNodeCommand ??= new Command(f =>
		{
			if (f is not Point) return;
			var position = (Point)f;
			if (App.ConditionsViewModel.CreatedElement == CreatedElements.Node)
			{
				var node = new Node(position);
				var viewNode = new GraphViewElement 
				{
					Geometry = CreatedDefaultEllipse(node.Position),
					GraphElement = node,
					ZIndex = 50
				};
				viewNode.Margin = new Thickness(position.X - radiusNode, position.Y - radiusNode, 0, 0);
				GraphElements.Add(viewNode);
			}
		}, f => App.ConditionsViewModel.CreatedElement == CreatedElements.Node);


		private ICommand addEdgeCommand;
		public ICommand AddEdgeCommand => addEdgeCommand ??= new Command(f =>
		{
			if (f is not Node) return;
			var node = (Node)f;
            if (tempEdge.Nodes[0] == null)
            {
                tempEdge.Nodes[0] = node;
            }
            else if (tempEdge.Nodes[1] == null)
            {
                tempEdge.Nodes[1] = node;
                var viewEdge = new GraphViewElement
                {
                    Geometry = CreatedDefaultLine(tempEdge.Nodes[0].Position, tempEdge.Nodes[1].Position),
					GraphElement = tempEdge,
					ZIndex = 45
                };
				var margin = new Thickness();
				if (tempEdge.Nodes[0].Position.X < tempEdge.Nodes[1].Position.X) margin.Left = tempEdge.Nodes[0].Position.X;
				else margin.Left = tempEdge.Nodes[1].Position.X;
                if (tempEdge.Nodes[0].Position.Y < tempEdge.Nodes[1].Position.Y) margin.Top = tempEdge.Nodes[0].Position.Y;
                else margin.Top = tempEdge.Nodes[1].Position.Y;
				viewEdge.Margin = margin;
                GraphElements.Add(viewEdge);
				tempEdge = new();
            }
        });


		private Geometry CreatedDefaultEllipse(Point position) => new EllipseGeometry { Center = position, RadiusX = radiusNode, RadiusY = radiusNode };
		private Geometry CreatedDefaultLine(Point start, Point end) => new LineGeometry { StartPoint = start, EndPoint = end };
	}
}
