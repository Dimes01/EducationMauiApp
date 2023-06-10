using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EducationMauiApp.ViewModels
{
    internal class ConditionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public bool IsEditingMode { get; set; }
    }
}
