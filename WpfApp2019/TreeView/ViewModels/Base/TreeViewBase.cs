using System.ComponentModel;

namespace WpfApp2019.TreeView
{
    public class TreeViewBase : INotifyPropertyChanged
    {
        // The event that is fired when any child property changes its value
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
