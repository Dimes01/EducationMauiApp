namespace EducationMauiApp.Interfaces
{
    internal interface IGraphViewElement
    {
        static readonly BindableProperty FillProperty;
        public Brush Fill
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        static readonly BindableProperty StrokeProperty;
        public Brush Stroke
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        
        static readonly BindableProperty StrokeThicknessProperty;
        public Brush StrokeThickness
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
