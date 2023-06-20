﻿using EducationMauiApp.Models;
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


		#region Свойства и поля

		private const double radiusNode = 10;
		private Edge tempEdge = new();
		private List<Node> nodesForRoute = new List<Node>();
		private List<GraphViewElement> elementsOfRoute = new List<GraphViewElement>();

		public ObservableCollection<GraphViewElement> GraphViewElements { get; private set; } = new ObservableCollection<GraphViewElement>();


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
			if (f is not Node) return;
			var node = (Node)f;
			var viewNode = new GraphViewElement
			{
				Geometry = CreatedDefaultEllipse(new Point(radiusNode, radiusNode)),
				GraphElement = node,
				ZIndex = ZIndexes.Node,
				Margin = new Thickness(node.Position.X - radiusNode, node.Position.Y - radiusNode, 0, 0)
			};
			GraphViewElements.Add(viewNode);
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
			tempEdge.Nodes[0].Neighbors.Add(tempEdge.Nodes[1]);
			if (!tempEdge.Oriented) tempEdge.Nodes[1].Neighbors.Add(tempEdge.Nodes[0]);
			SetEdgeToElements(tempEdge);			
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
			if (WorkingNode == null) return;
			List<GraphViewElement> removeEdges = new();
			for (int i = 0; i < GraphViewElements.Count; ++i)
			{
				if (GraphViewElements[i].GraphElement is not Edge) continue;
				var edge = GraphViewElements[i].GraphElement as Edge;
				if (edge.Nodes[0] == WorkingNode.GraphElement || edge.Nodes[1] == WorkingNode.GraphElement)
					removeEdges.Add(GraphViewElements[i]);
			}
			for (int i = 0; i < removeEdges.Count; ++i)
			{
				GraphViewElements.Remove(removeEdges[i]);
				removeEdges[i].GraphElement.Remove();
			}

            // не работает из-за различий в свойствах Bounds и Frame между элементами коллекции и WorkingNode
			//GraphViewElements.Remove(WorkingNode);  

            for (int i = 0; i < GraphViewElements.Count; ++i)
			{ 
				if (GraphViewElements[i].GraphElement != WorkingNode.GraphElement) continue;
				GraphViewElements.RemoveAt(i);
				break;
			}
			WorkingNode.GraphElement.Remove();
			WorkingNode = null;
		});


		private ICommand addToEndOfRouteCommand;
		public ICommand AddToEndOfRouteCommand => addToEndOfRouteCommand ??= new Command(f =>
		{
			if (f is not GraphViewElement) return;
			var element = ((GraphViewElement)f).GraphElement;
			if (element is not Node) return;
			nodesForRoute.Add(element as Node);
		});


		private ICommand removeFromRouteCommand;
		public ICommand RemoveFromRouteCommand => removeFromRouteCommand ??= new Command(f =>
		{
			if (f is not GraphViewElement) return;
			var element = ((GraphViewElement)f).GraphElement;
			if (element is not Node) return;
			nodesForRoute.Remove(element as Node);
		});


		private ICommand makeRouteCommand;
		public ICommand MakeRouteCommand => makeRouteCommand ??= new Command(f =>
		{
			if (elementsOfRoute.Count > 0 || nodesForRoute.Count < 2)
			{
				RemoveRouteCommand.Execute(null);
				return;
			}
			GraphViewElement element;
			var listRoutes = new List<Node>();
			for (int i = 0; i < nodesForRoute.Count - 1; ++i)
			{
				listRoutes = Route.GetRoute(nodesForRoute[i], nodesForRoute[i + 1]);
				for (int j = 0; j < listRoutes.Count - 1; ++j)
				{
					SetNodeOfRouteToElements(listRoutes[j]);
					SetEdgeToElements(new Edge(listRoutes[j], listRoutes[j + 1]));
					element = GraphViewElements.Last();
					elementsOfRoute.Add(element);
					element.ZIndex = ZIndexes.RouteEdge;
					element.Stroke = Brush.Green;
					element.StrokeThickness = 4;
				}
			}
			SetNodeOfRouteToElements(nodesForRoute.Last());
		});


		private ICommand removeRouteCommand;
		public ICommand RemoveRouteCommand => removeRouteCommand ??= new Command(f =>
		{
			for (int i = 0; i < elementsOfRoute.Count; ++i) 
				GraphViewElements.Remove(elementsOfRoute[i]);
			elementsOfRoute.Clear();
			nodesForRoute.Clear();
		});



		#endregion

		#region Вспомогательные методы

		private Geometry CreatedDefaultEllipse(Point position) => new EllipseGeometry { Center = position, RadiusX = radiusNode, RadiusY = radiusNode };
		private Geometry CreatedDefaultLine(Point start, Point end)
		{
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
		private void SetNodeOfRouteToElements(Node node)
		{
			AddNodeCommand.Execute(node);
			var element = GraphViewElements.Last();
			element.Fill = Brush.Red;
			element.ZIndex = ZIndexes.RouteNode;
			elementsOfRoute.Add(GraphViewElements.Last());
		}
		private void SetEdgeToElements(Edge edge)
		{
			var minOfCoordinates = new Point();
			if (edge.Nodes[0].Position.X < edge.Nodes[1].Position.X) minOfCoordinates.X = edge.Nodes[0].Position.X;
			else minOfCoordinates.X = edge.Nodes[1].Position.X;
			if (edge.Nodes[0].Position.Y < edge.Nodes[1].Position.Y) minOfCoordinates.Y = edge.Nodes[0].Position.Y;
			else minOfCoordinates.Y = edge.Nodes[1].Position.Y;

			var pointStart = new Point(edge.Nodes[0].Position.X - minOfCoordinates.X, edge.Nodes[0].Position.Y - minOfCoordinates.Y);
			var pointEnd = new Point(edge.Nodes[1].Position.X - minOfCoordinates.X, edge.Nodes[1].Position.Y - minOfCoordinates.Y);
			var viewEdge = new GraphViewElement
			{
				Geometry = CreatedDefaultLine(pointStart, pointEnd),
				GraphElement = edge,
				ZIndex = ZIndexes.Edge,
				Margin = new Thickness(minOfCoordinates.X, minOfCoordinates.Y, 0, 0),
			};
			GraphViewElements.Add(viewEdge);
		}

		#endregion
	}
}
