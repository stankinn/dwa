using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.Model;

namespace WpfApp2019.ViewModel
{
    internal class PathViewModel : ObservableObject
    {
        
        public PathViewModel()
        {
           
        }

        private ICommand _changeTextCommand;
        public ICommand ChangeTextCommand
        {
            get
            {
                if (_changeTextCommand == null)
                {
                    _changeTextCommand = new RelayCommand(
                        param => this.SearchFiles()
                    );
                }
                return _changeTextCommand;
            }

        }

        private PathText _filePathText;
        public PathText FilePathText
        {

            get => _filePathText;
            set
            {
                if (_filePathText != value)
                {
                    _filePathText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SearchFiles()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.Desktop
            };

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                //Path wird gesetzt
                string sPath = folderDialog.SelectedPath;

                FilePathText = new PathText
                {
                    FPath = sPath
                };
                
                Trace.WriteLine("click: " + FilePathText.FPath);

            }
        }


       


    }
}
