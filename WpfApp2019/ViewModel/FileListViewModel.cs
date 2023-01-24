using Prism.Events;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfApp2019.AppServices;
using WpfApp2019.Model;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace WpfApp2019.ViewModel
{
    internal class FileListViewModel : ViewModelBase
    {
        IEventAggregator _ea = ApplicationService.Instance.EventAggregator;
        public FileListViewModel( )
        {
            ApplicationService.Instance.EventAggregator.GetEvent<PathChangedEvent>().Subscribe(LoadObjects);
            ApplicationService.Instance.EventAggregator.GetEvent<SearchChangedEvent>().Subscribe(Search);
            //LoadObjects();
        }

        private bool _isEditable;
        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                _isEditable = value;
                OnPropertyChanged();
                Trace.WriteLine("Is now editable: " + value);
            }
        }


        public ObservableCollection<Item> Items { get; set; }

        //Änderung/Aktualisierung der Attribute
        private ObjectAttributes _object = new ObjectAttributes();
        public ObjectAttributes Object
        {

            get => _object;
            set
            {
                if (_object != value)
                {
                    _object = value;
                    OnPropertyChanged();
                }
            }
        }

        //Änderung/Aktualisierung der ganzen Collection
        private ObservableCollection<ObjectAttributes> _files = new ObservableCollection<ObjectAttributes>();
        public ObservableCollection<ObjectAttributes> Files
        {
            get => _files;
            set
            {
                if (_files != value)
                {
                    _files = value;
                    OnPropertyChanged();
                    _ea.GetEvent<StatusChangedEvent>().Publish(_files.Count);
                }
            }
        }
        private ICommand _openFile;
        public ICommand OpenFile
        {
            get
            {
                if (_openFile == null)
                {
                    _openFile = new RelayCommand(
                        param => this.Open(param as ObjectAttributes)
                    );
                }
                return _openFile;
            }

        }
        private ICommand _delete;
        public ICommand DeleteCommand
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommand(
                        param => this.Delete(param as ObjectAttributes)
                    );
                }
                return _delete;
            }

        }

        public void Delete(ObjectAttributes obj)
        {
            File.Delete(obj.FilePath);
            Files.Remove(obj);
            Trace.WriteLine("File deleted: " + obj.Name);
        }

        private ICommand _copy;
        public ICommand CopyCommand
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(
                        param => this.Copy(param as ObjectAttributes)
                    );
                }
                return _copy;
            }

        }

        public void Copy(ObjectAttributes obj)
        {
            
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Copy the selected file to the destination folder
                if (Path.Combine(dialog.SelectedPath, obj.Name + obj.Type) == null)
                {
                    File.Copy(obj.FilePath, Path.Combine(dialog.SelectedPath, obj.Name + obj.Type));
                }
                else {

                    File.Copy(obj.FilePath, Path.Combine(dialog.SelectedPath, obj.Name + "_copy" +obj.Type));
                }

            }
            Trace.WriteLine(obj.FilePath);
            LoadObjects(obj.FilePath.Replace(obj.Name+obj.Type, ""));
        }

        private ICommand _rename;
        public ICommand RenameCommand
        {
            get
            {
                if (_rename == null)
                {
                    _rename = new RelayCommand(
                        param => this.Rename(param as ObjectAttributes)
                    );
                }
                return _rename;
            }

        }

        public void Rename(ObjectAttributes obj)
        {
            string name = obj.Name;
            IsEditable = true;
            Trace.WriteLine("Rename this" + name);
        }

        public void Open(ObjectAttributes objects)
        {
            string path = objects.FilePath;

            //Navigation bei Dateiordnern, Dateien werden geöffnet
            if (objects.Type == "Dateiordner")
            {
                //var files = Directory.EnumerateFileSystemEntries(path);
                //ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                //foreach (var d in files)
                //{
                //    var accessControl = new FileInfo(d).GetAccessControl();
                //    items.Add(new ObjectAttributes
                //    {
                //        Name = Path.GetFileName(d),
                //        Type = GetDataType(d),
                //        ModificationTime = Directory.GetLastWriteTime(d),
                //        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                //        Description = GetFileDescription(d),
                //        FilePath = Path.GetFullPath(d)
                //    });

                //}
                //Files = items;

                _ea.GetEvent<DirectoryChangedEvent>().Publish(path);

            }
            else
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(@$"{path}")
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }

        }

        public void Search(SearchParameters search)
        {

            string path = search.Path.FPath;
            string sString = search.SearchInput.ToLower();
            var files = Directory.EnumerateFileSystemEntries(path);
            ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
            foreach (var d in files)
            {
                var accessControl = new FileInfo(d).GetAccessControl();


                if (Path.GetFileName(d).ToLower().Contains(sString) || GetDataType(d).ToLower().Contains(sString) || accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString().ToLower().Contains(sString))
                {
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d).Replace(GetDataType(d), ""),
                        Type = GetDataType(d),
                        ModificationTime = Directory.GetLastWriteTime(d),
                        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                        Description = GetFileDescription(d),
                        FilePath = Path.GetFullPath(d)
                    });
                }

            }
            Files = items;

        }

        private ICommand _createMDCommand;
        public ICommand CreateMDCommand
        {
            get
            {
                if (_createMDCommand == null)
                {
                    _createMDCommand = new RelayCommand(
                        param => this.CreateMD(param as ObjectAttributes)
                    );
                }
                return _createMDCommand;
            }
        }

        public void CreateMD(ObjectAttributes obj)
        {

            string directoryPath = obj.FilePath.Replace($"\\{obj.Name}", "");

            if (obj.Type == ".csv")
            {
                Trace.WriteLine("Clicked Name: " + obj.Name +  " path: " + obj.FilePath);
                //var accessControl = new FileInfo(obj.Name).GetAccessControl();

                IRazorEngine razorEngine = new RazorEngine();

                string templateTextMD = File.ReadAllText("..\\..\\..\\TemplateLegoMD.txt");

                if (obj.Name.Contains(".csv"))
                {
                    List<string> dataTypes = new List<string>();
                    List<string> dataNames = new List<string>();

                    using (var reader = new StreamReader(obj.FilePath))
                    {
                        List<string> listA = new List<string>();
                        List<string> listB = new List<string>();
                        int j = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            if (j == 0)
                            {
                                foreach (var item in values)
                                {
                                    if (item.Contains(";"))
                                    {
                                        string itema = item.Replace(";", string.Empty);
                                        dataNames.Add(itema);
                                    }
                                    else
                                    {
                                        dataNames.Add(item);
                                    }
                                    dataTypes.Add(" ");
                                }
                            }
                            else if (j < 10)
                            {
                                int k = 0;
                                try
                                {
                                    for (int i = 0; i < values.Length; i++)
                                    {
                                        if (k >= dataTypes.Count)
                                        {
                                            break;
                                        }
                                        if (dataTypes[k] != "string" || dataTypes[k] != "double")
                                        {
                                            if (values[i].Contains(";"))
                                            {
                                                break;
                                            }
                                            if (values[i] == null || values[i] == "")
                                            {
                                                break;
                                            }
                                            if (dataTypes[k] == " ")
                                            {
                                                int intValue;
                                                double doubleValue;
                                                bool boolValue;

                                                if (int.TryParse(values[i], out intValue))
                                                    dataTypes[k] = "int";
                                                else if (double.TryParse(values[i], out doubleValue))
                                                    dataTypes[k] = "double";
                                                else if (bool.TryParse(values[i], out boolValue))
                                                    dataTypes[k] = "bool";
                                                else dataTypes[k] = "string";
                                            }
                                            if (dataTypes[k] == "int")
                                            {
                                                int intValue;
                                                double doubleValue;

                                                if (!int.TryParse(values[i], out intValue))
                                                {
                                                    if (double.TryParse(values[i], out doubleValue))
                                                        dataTypes[k] = "double";
                                                }
                                            }
                                        }
                                        k++;
                                    }
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine(k + " " + j);
                                    Debug.WriteLine(dataTypes.Count + " " + values.Length);
                                    throw;
                                }


                            }
                            j++;
                        }
                    }
                    var model = new lego(GetFileDescription(obj.FilePath), Directory.GetLastWriteTime(obj.FilePath), dataTypes, dataNames);
                   

                    Trace.WriteLine("Überordner: " + directoryPath);
                    if (obj.Name.Contains("themes"))
                    {
                        string templateText2 = File.ReadAllText("..\\..\\..\\TemplateLegoTheme.txt");

                        IRazorEngineCompiledTemplate template2 = razorEngine.Compile(templateText2);
                        var resultText2 = template2.Run(model);
                        File.WriteAllText($"{directoryPath}\\LegoThemeSql.sql", resultText2);

                        IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                        var resultTextMD = templateMD.Run(model);
                        File.WriteAllText($"{directoryPath}\\LegoThemeMD.md", resultTextMD);
                    }
                    else if (obj.Name.Contains("sets"))
                    {
                        string templateText = File.ReadAllText("..\\..\\..\\TemplateLegoSet.txt");

                        IRazorEngineCompiledTemplate template = razorEngine.Compile(templateText);
                        var resultText = template.Run(model);
                        File.WriteAllText($"{directoryPath}\\LegoSetSql.sql", resultText);

                        IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                        var resultTextMD = templateMD.Run(model);
                        File.WriteAllText($"{directoryPath}\\LegoSetMD.md", resultTextMD);

                    }
                }

                //JSONparser irgendwas was auch immer 
                string json = File.ReadAllText(obj.FilePath);
            } else
            {
                MessageBox.Show("Not a .csv file!");
            }

            //neue files hinzugefügt zu items
            var files = Directory.EnumerateFileSystemEntries(directoryPath);
            ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
            ObservableCollection<Item> treeItems = new ObservableCollection<Item>();

            foreach (var d in files)
            {
                var accessControl = new FileInfo(d).GetAccessControl();
                items.Add(new ObjectAttributes
                {
                    Name = Path.GetFileName(d).Replace(GetDataType(d), ""),
                    Type = GetDataType(d),
                    ModificationTime = Directory.GetLastWriteTime(d),
                    Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                    Description = GetFileDescription(d),
                    FilePath = Path.GetFullPath(d)
                });

                treeItems.Add(new Item
                {
                    Title = Path.GetFileName(d)
                });
            }
            Files = items;

        }

        public void LoadObjects(string path)
        {
            
            try
            {
               
                var files = Directory.EnumerateFileSystemEntries(path);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                ObservableCollection<Item> treeItems = new ObservableCollection<Item>();

                #region comment
                //foreach (var d in files)
                //{
                //    var accessControl = new FileInfo(d).GetAccessControl();

                //    IRazorEngine razorEngine = new RazorEngine();

                //    string templateTextMD = File.ReadAllText("..\\..\\..\\TemplateLegoMD.txt");

                //    if (Path.GetFileName(d).Contains(".csv"))
                //    {
                //        List<string> dataTypes = new List<string>();
                //        List<string> dataNames = new List<string>();

                //        using (var reader = new StreamReader(d))
                //        {
                //            List<string> listA = new List<string>();
                //            List<string> listB = new List<string>();
                //            int j = 0;
                //            while (!reader.EndOfStream)
                //            {
                //                var line = reader.ReadLine();
                //                var values = line.Split(',');

                //                if (j == 0)
                //                {
                //                    foreach (var item in values)
                //                    {
                //                        if (item.Contains(";"))
                //                        {
                //                            string itema= item.Replace(";", string.Empty);
                //                            dataNames.Add(itema);
                //                        }
                //                        else
                //                        {
                //                            dataNames.Add(item);
                //                        }
                //                        dataTypes.Add(" ");
                //                    }
                //                }
                //                else if(j < 10)
                //                {
                //                    int k = 0;
                //                    try
                //                    {
                //                        for (int i = 0; i < values.Length; i++)
                //                        {
                //                            if(k >= dataTypes.Count) 
                //                            {
                //                                break; 
                //                            }
                //                            if (dataTypes[k] != "string" || dataTypes[k] != "double")
                //                            {
                //                                if (values[i].Contains(";"))
                //                                {
                //                                    break;
                //                                }
                //                                if(values[i] == null || values[i] == "")
                //                                {
                //                                break;
                //                                }
                //                                if (dataTypes[k] == " ")
                //                                {
                //                                    int intValue;
                //                                    double doubleValue;
                //                                    bool boolValue;

                //                                    if (int.TryParse(values[i], out intValue))
                //                                        dataTypes[k] = "int";
                //                                    else if (double.TryParse(values[i], out doubleValue))
                //                                        dataTypes[k] = "double";
                //                                    else if (bool.TryParse(values[i], out boolValue))
                //                                        dataTypes[k] = "bool";
                //                                    else dataTypes[k] = "string";
                //                                }
                //                                if (dataTypes[k] == "int")
                //                                {
                //                                    int intValue;
                //                                    double doubleValue;

                //                                    if (!int.TryParse(values[i], out intValue))
                //                                    {
                //                                        if (double.TryParse(values[i], out doubleValue))
                //                                            dataTypes[k] = "double";
                //                                    }
                //                                }
                //                            }
                //                            k++;
                //                        }
                //                    }
                //                    catch (Exception)
                //                    {
                //                        Debug.WriteLine(k + " " + j);
                //                        Debug.WriteLine(dataTypes.Count + " " + values.Length);
                //                        throw;
                //                    }


                //                }
                //                j++;
                //            }
                //        }
                //        var model = new lego(GetFileDescription(d), Directory.GetLastWriteTime(d), dataTypes, dataNames);

                //        if (Path.GetFileName(d).Contains("themes"))
                //        {
                //            string templateText2 = File.ReadAllText("..\\..\\..\\TemplateLegoTheme.txt");

                //            IRazorEngineCompiledTemplate template2 = razorEngine.Compile(templateText2);
                //            var resultText2 = template2.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoThemeSql.sql" , resultText2);

                //            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                //            var resultTextMD = templateMD.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoThemeMD.md", resultTextMD);
                //        }
                //        else if (Path.GetFileName(d).Contains("sets"))
                //        {
                //            string templateText = File.ReadAllText("..\\..\\..\\TemplateLegoSet.txt");

                //            IRazorEngineCompiledTemplate template = razorEngine.Compile(templateText);
                //            var resultText = template.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoSetSql.sql", resultText);

                //            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                //            var resultTextMD = templateMD.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoSetMD.md", resultTextMD);

                //        }        
                //    }

                //    //if (Path.GetFileName(d).Contains(".JSON"))
                //    //{
                //    //JSONparser irgendwas was auch immer 
                //    string json = File.ReadAllText(d);
                //    //}
                //    //if (Path.GetFileName(d).Contains(".XML"))
                //    //{

                //    //}
                //    //if (Path.GetFileName(d).Contains(".exel"))
                //    //{

                //    //}

                //    // SqlCommand cmd = new SqlCommand("INSERT INTO USERS(username, email, phone) values ('" + EntityName.Text + "','" + AttributeType.Text + "','" + AttributeDataType.Text + "')", con);
                //    // cmd.ExecuteNonQuery();
                //    // con.Close();
                //}                //    IRazorEngine razorEngine = new RazorEngine();

                //    string templateTextMD = File.ReadAllText("..\\..\\..\\TemplateLegoMD.txt");

                //    if (Path.GetFileName(d).Contains(".csv"))
                //    {
                //        List<string> dataTypes = new List<string>();
                //        List<string> dataNames = new List<string>();

                //        using (var reader = new StreamReader(d))
                //        {
                //            List<string> listA = new List<string>();
                //            List<string> listB = new List<string>();
                //            int j = 0;
                //            while (!reader.EndOfStream)
                //            {
                //                var line = reader.ReadLine();
                //                var values = line.Split(',');

                //                if (j == 0)
                //                {
                //                    foreach (var item in values)
                //                    {
                //                        if (item.Contains(";"))
                //                        {
                //                            string itema= item.Replace(";", string.Empty);
                //                            dataNames.Add(itema);
                //                        }
                //                        else
                //                        {
                //                            dataNames.Add(item);
                //                        }
                //                        dataTypes.Add(" ");
                //                    }
                //                }
                //                else if(j < 10)
                //                {
                //                    int k = 0;
                //                    try
                //                    {
                //                        for (int i = 0; i < values.Length; i++)
                //                        {
                //                            if(k >= dataTypes.Count) 
                //                            {
                //                                break; 
                //                            }
                //                            if (dataTypes[k] != "string" || dataTypes[k] != "double")
                //                            {
                //                                if (values[i].Contains(";"))
                //                                {
                //                                    break;
                //                                }
                //                                if(values[i] == null || values[i] == "")
                //                                {
                //                                break;
                //                                }
                //                                if (dataTypes[k] == " ")
                //                                {
                //                                    int intValue;
                //                                    double doubleValue;
                //                                    bool boolValue;

                //                                    if (int.TryParse(values[i], out intValue))
                //                                        dataTypes[k] = "int";
                //                                    else if (double.TryParse(values[i], out doubleValue))
                //                                        dataTypes[k] = "double";
                //                                    else if (bool.TryParse(values[i], out boolValue))
                //                                        dataTypes[k] = "bool";
                //                                    else dataTypes[k] = "string";
                //                                }
                //                                if (dataTypes[k] == "int")
                //                                {
                //                                    int intValue;
                //                                    double doubleValue;

                //                                    if (!int.TryParse(values[i], out intValue))
                //                                    {
                //                                        if (double.TryParse(values[i], out doubleValue))
                //                                            dataTypes[k] = "double";
                //                                    }
                //                                }
                //                            }
                //                            k++;
                //                        }
                //                    }
                //                    catch (Exception)
                //                    {
                //                        Debug.WriteLine(k + " " + j);
                //                        Debug.WriteLine(dataTypes.Count + " " + values.Length);
                //                        throw;
                //                    }


                //                }
                //                j++;
                //            }
                //        }
                //        var model = new lego(GetFileDescription(d), Directory.GetLastWriteTime(d), dataTypes, dataNames);

                //        if (Path.GetFileName(d).Contains("themes"))
                //        {
                //            string templateText2 = File.ReadAllText("..\\..\\..\\TemplateLegoTheme.txt");

                //            IRazorEngineCompiledTemplate template2 = razorEngine.Compile(templateText2);
                //            var resultText2 = template2.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoThemeSql.sql" , resultText2);

                //            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                //            var resultTextMD = templateMD.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoThemeMD.md", resultTextMD);
                //        }
                //        else if (Path.GetFileName(d).Contains("sets"))
                //        {
                //            string templateText = File.ReadAllText("..\\..\\..\\TemplateLegoSet.txt");

                //            IRazorEngineCompiledTemplate template = razorEngine.Compile(templateText);
                //            var resultText = template.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoSetSql.sql", resultText);

                //            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                //            var resultTextMD = templateMD.Run(model);
                //            File.WriteAllText($"{sPath}\\LegoSetMD.md", resultTextMD);

                //        }        
                //    }

                //    //if (Path.GetFileName(d).Contains(".JSON"))
                //    //{
                //    //JSONparser irgendwas was auch immer 
                //    string json = File.ReadAllText(d);
                //    //}
                //    //if (Path.GetFileName(d).Contains(".XML"))
                //    //{

                //    //}
                //    //if (Path.GetFileName(d).Contains(".exel"))
                //    //{

                //    //}

                //    // SqlCommand cmd = new SqlCommand("INSERT INTO USERS(username, email, phone) values ('" + EntityName.Text + "','" + AttributeType.Text + "','" + AttributeDataType.Text + "')", con);
                //    // cmd.ExecuteNonQuery();
                //    // con.Close();
                //}

                #endregion

                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d).Replace(GetDataType(d), ""),
                        Type = GetDataType(d),
                        ModificationTime = Directory.GetLastWriteTime(d),
                        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                        Description = GetFileDescription(d),
                        FilePath = Path.GetFullPath(d)
                    });

                    treeItems.Add(new Item
                    {
                        Title = Path.GetFileName(d)
                    });
                }
                Files = items;
                Items = treeItems;
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private string GetDataType(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return Path.GetExtension(file);
            }
            else
            {
                return "Dateiordner";
            }
        }

        private string GetFileDescription(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return FileVersionInfo.GetVersionInfo(file).FileDescription;
            }
            else
            {
                return "";
            }
        }

    }
}
