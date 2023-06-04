using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EducationMauiApp.ViewModels
{
    internal class ConditionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public CreatedElements CreatedElement { get; set; }
    }

    public enum CreatedElements
    {
        Node, Edge
    };
}
