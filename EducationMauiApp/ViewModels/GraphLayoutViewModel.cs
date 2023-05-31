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
        private Geometry CreatedGeometry { get; set; }
        public ObservableCollection<NodeElement> Nodes { get; private set; } = new ObservableCollection<NodeElement>();


        private ICommand addGraphElement;
        public ICommand AddGraphElement => addGraphElement ??= new Command(f =>
        {
            Point position;
            if (f is not Point) return;
            position = (Point)f;
            CreatedDefaultGeometry(position);
            Nodes.Add(new NodeElement { Geometry = CreatedGeometry });
        });

        private void CreatedDefaultGeometry(Point position)
        {
            CreatedGeometry = new EllipseGeometry
            {
                Center = position,
                RadiusX = 10,
                RadiusY = 10
            };
        }
    }
}
