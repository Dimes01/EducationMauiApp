namespace EducationMauiApp.UIElements;

public partial class PanAndZoomView : ContentView
{
	public PanAndZoomView()
	{
		InitializeComponent();
	}

    private double currentScale = 1;
    private double startScale = 1;
    private double xOffset = 0;
    private double yOffset = 0;
    private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        var content = (ContentView)sender;
        if (e.Status == GestureStatus.Started)
        {
            startScale = content.Scale;
            content.AnchorX = 0;
            content.AnchorY = 0;
        }
        else if (e.Status == GestureStatus.Running)
        {
            currentScale = (e.Scale - 1) * startScale;
            currentScale = Math.Max(currentScale, 1);

            double renderedX = content.X + xOffset;
            double deltaX = renderedX / Width;
            double deltaWidth = Width / (content.Width * startScale);
            double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

            double renderedY = content.Y + yOffset;
            double deltaY = renderedY / Height;
            double deltaHeight = Height / (content.Height * startScale);
            double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

            double targetX = xOffset - (originX * content.Width) * (currentScale - startScale);
            double targetY = yOffset - (originY * content.Height) * (currentScale - startScale);

            content.TranslationX = Math.Clamp(targetX, -content.Width * (currentScale - 1), 0);
            content.TranslationY = Math.Clamp(targetY, -content.Height * (currentScale - 1), 0);

            content.Scale = currentScale;
        }
        else if (e.Status == GestureStatus.Completed)
        {
            xOffset = content.TranslationX;
            yOffset = content.TranslationY;
        }
    }
}