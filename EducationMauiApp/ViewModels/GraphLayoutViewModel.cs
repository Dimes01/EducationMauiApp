using EducationMauiApp.UIElements;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EducationMauiApp.ViewModels
{
    internal class GraphLayoutViewModel
    {
        private Point[] edgesPoint = new Point[] { Point.Zero, Point.Zero };
        public ObservableCollection<NodeElement> Nodes { get; private set; } = new ObservableCollection<NodeElement>();

        private ICommand addGraphElementCommand;
        public ICommand AddGraphElementCommand => addGraphElementCommand ??= new Command(f =>
        {
            if (f is not Point) return;
            var position = (Point)f;
            if (App.ConditionsViewModel.CreatedElement == CreatedElements.Node)
            {
                Nodes.Add(new NodeElement { Geometry = CreatedDefaultEllipse(position) });
            }
            else if (edgesPoint[0] == Point.Zero)
            {
                edgesPoint[0] = position;
            }
            else if (edgesPoint[1] == Point.Zero)
            {
                edgesPoint[1] = position;

            }
        });

        private Geometry CreatedDefaultEllipse(Point position) => new EllipseGeometry { Center = position, RadiusX = 10, RadiusY = 10 };
        private Geometry CreatedDefaultLine(Point start, Point end) => new LineGeometry { StartPoint = start, EndPoint = end };
    }
}
