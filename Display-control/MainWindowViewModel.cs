using System.ComponentModel;

namespace Display_control
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {

        }

        public int Height { get; set; }
        public int Width { get; set; }
    }
}
