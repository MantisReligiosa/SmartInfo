using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DomainObjects.Blocks;

namespace Display_control
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<DisplayBlock> Blocks { get; internal set; }

        public MainWindowViewModel()
        {

        }

        public WindowState WindowState;
    }
}
