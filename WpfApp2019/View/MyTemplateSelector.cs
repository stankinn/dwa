using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfApp2019.ViewModel;

namespace WpfApp2019.View
{
    internal class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EditableTemplate { get; set; }
        public DataTemplate NonEditableTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var vm = new FileListViewModel();
            return vm.IsEditable ? EditableTemplate : NonEditableTemplate;
        }
    }
}
