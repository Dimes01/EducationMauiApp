using EducationMauiApp.UIElements;
using System.Collections.ObjectModel;

namespace EducationMauiApp.Models
{
    internal class GraphViewElementsCollection
    {
        private readonly ObservableCollection<GraphViewElement> _elements = new();
        public ObservableCollection<GraphViewElement> Elements { get => _elements; }

        public void AddElement(GraphViewElement element) => _elements.Add(element);
        public void RemoveElement(GraphViewElement element)
        {
            _elements.Remove(element);
            element.GraphElement.Remove();
        }
    }
}
