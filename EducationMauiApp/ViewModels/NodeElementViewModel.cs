//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace EducationMauiApp.ViewModels
//{
//    internal class NodeElementViewModel : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


//        private Brush fill = (Brush)App.Current.Resources.MergedDictionaries.ElementAt(0)["FillNodeBrush"];
//        public Brush Fill
//        {
//            get => fill;
//            set
//            {
//                if (fill == value) return;
//                fill = value;
//                OnPropertyChanged(nameof(Fill));
//            }
//        }


//        private Brush stroke = (Brush)App.Current.Resources.MergedDictionaries.ElementAt(0)["StrokeNodeBrush"];
//        public Brush Stroke
//        {
//            get => stroke;
//            set
//            {
//                if (stroke == value) return;
//                stroke = value;
//                OnPropertyChanged(nameof(Stroke));
//            }
//        }


//        private double strokeThickness;
//        public double StrokeThickness
//        {
//            get => strokeThickness;
//            set
//            {
//                if (strokeThickness == value) return;
//                strokeThickness = value;
//                OnPropertyChanged(nameof(StrokeThickness));
//            }
//        }


//        private Point position;
//        public Point Position
//        {
//            get => position;
//            set
//            {
//                if (position == value) return;
//                position = value;
//                OnPropertyChanged(nameof(Position));
//            }
//        }


//        private double radius = 10;
//        public double Radius
//        {
//            get => radius;
//            private set
//            {
//                if (radius == value) return;
//                radius = value;
//                SelectRadius = radius + 2;
//                OnPropertyChanged(nameof(Radius));
//            }
//        }


//        private double selectRadius = 12;
//        public double SelectRadius
//        {
//            get => selectRadius;
//            private set
//            {
//                if (selectRadius == value) return;
//                selectRadius = value;
//                OnPropertyChanged(nameof(SelectRadius));
//            }
//        }


//        private double selectedStrokeThickness;
//        public double SelectedStrokeThickness
//        {
//            get => selectedStrokeThickness;
//            private set
//            {
//                if (selectedStrokeThickness == value) return;
//                selectedStrokeThickness = value;
//                OnPropertyChanged(nameof(SelectedStrokeThickness));
//            }
//        }


//        private bool isSelected = true;
//        public bool IsSelected
//        {
//            get => isSelected;
//            set
//            {
//                if (isSelected == value) return;
//                isSelected = value;
//                OnPropertyChanged(nameof(IsSelected));
//            }
//        }
//    }
//}
