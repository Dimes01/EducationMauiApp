using EducationMauiApp.Interfaces;
using EducationMauiApp.UIElements;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EducationMauiApp.ViewModels
{
    internal class GraphLayoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private Point[] edgesPoint = new Point[] { Point.Zero, Point.Zero };
        public ObservableCollection<IGraphViewElement> GraphElements { get; private set; } = new ObservableCollection<IGraphViewElement>();

        public NodeElement SelectedNode { get; set; }



        //private Point position = new Point(100, 150);
        //public Point Position
        //{
        //    get => position;
        //    set
        //    {
        //        if (position == value) return;
        //        position = value;
        //        OnPropertyChanged(nameof(Position));
        //    }
        //}



        private ICommand addGraphElementCommand;
        public ICommand AddGraphElementCommand => addGraphElementCommand ??= new Command(f =>
        {
            if (f is not Point) return;
            var position = (Point)f;
            if (App.ConditionsViewModel.CreatedElement == CreatedElements.Node)
            {
                var node = new NodeElement { Position = position };
                GraphElements.Add(node);
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
