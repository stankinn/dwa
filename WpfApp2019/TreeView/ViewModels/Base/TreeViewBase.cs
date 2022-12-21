using System.ComponentModel;

namespace WpfApp2019.TreeView
{
    public class TreeViewBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
