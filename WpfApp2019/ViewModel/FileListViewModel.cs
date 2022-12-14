using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using WpfApp2019.Model;

namespace WpfApp2019.ViewModel
{
    internal class FileListViewModel : ObservableObject, IViewModel
    {
        public FileListViewModel( )
        {
            ApplicationService.Instance.EventAggregator.GetEvent<PathChangedEvent>().Subscribe(LoadObjects);
            //LoadObjects();
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
                    Trace.WriteLine("value changed");
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
                    Trace.WriteLine("value changed");
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
            Trace.WriteLine("Rename this" + obj.Name);
        }

        public void Open(ObjectAttributes objects)
        {
            string path = objects.FilePath;

            //Navigation bei Dateiordnern, Dateien werden geöffnet
            if (objects.Type == "Dateiordner")
            {
                var files = Directory.EnumerateFileSystemEntries(path);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d),
                        Type = GetDataType(d),
                        ModificationTime = Directory.GetLastWriteTime(d),
                        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                        Description = GetFileDescription(d),
                        FilePath = Path.GetFullPath(d)
                    });

                }
                Files = items;

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

        public void LoadObjects(PathText path)
        {
            string sPath = "";
            //Trace.WriteLine("LOADING OBJECTS");

            if (path != null)
            {
                sPath = path.FPath;

            }
            Trace.WriteLine("pp: " + sPath);
            try
            {
               
                var files = Directory.EnumerateFileSystemEntries(sPath);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                ObservableCollection<Item> treeItems = new ObservableCollection<Item>();
                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    //items.Add(new ObjectAttributes
                    //{
                    //    Name = Path.GetFileName(d),
                    //    Type = GetDataType(d),
                    //    ModificationTime = Directory.GetLastWriteTime(d),
                    //    Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                    //    Description = GetFileDescription(d),
                    //    FilePath = Path.GetFullPath(d)
                    //});

                    //con.Open();

                    IRazorEngine razorEngine = new RazorEngine();
                    
                    string templateTextMD = File.ReadAllText("..\\..\\..\\TemplateLegoMD.txt");

                    if (Path.GetFileName(d).Contains(".csv"))
                    {
                        List<string> dataTypes = new List<string>();
                        List<string> dataNames = new List<string>();

                        using (var reader = new StreamReader(d))
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
                                            string itema= item.Replace(";", string.Empty);
                                            dataNames.Add(itema);
                                        }
                                        else
                                        {
                                            dataNames.Add(item);
                                        }
                                        dataTypes.Add(" ");
                                    }
                                }
                                else if(j < 10)
                                {
                                    int k = 0;
                                    try
                                    {
                                        for (int i = 0; i < values.Length; i++)
                                        {
                                            if(k >= dataTypes.Count) 
                                            {
                                                break; 
                                            }
                                            if (dataTypes[k] != "string" || dataTypes[k] != "double")
                                            {
                                                if (values[i].Contains(";"))
                                                {
                                                    break;
                                                }
                                                if(values[i] == null || values[i] == "")
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
                        var model = new lego(GetFileDescription(d), Directory.GetLastWriteTime(d), dataTypes, dataNames);

                        if (Path.GetFileName(d).Contains("themes"))
                        {
                            string templateText2 = File.ReadAllText("..\\..\\..\\TemplateLegoTheme.txt");

                            IRazorEngineCompiledTemplate template2 = razorEngine.Compile(templateText2);
                            var resultText2 = template2.Run(model);
                            File.WriteAllText($"{sPath}\\LegoThemeSql.sql" , resultText2);

                            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                            var resultTextMD = templateMD.Run(model);
                            File.WriteAllText($"{sPath}\\LegoThemeMD.md", resultTextMD);
                        }
                        else if (Path.GetFileName(d).Contains("sets"))
                        {
                            string templateText = File.ReadAllText("..\\..\\..\\TemplateLegoSet.txt");

                            IRazorEngineCompiledTemplate template = razorEngine.Compile(templateText);
                            var resultText = template.Run(model);
                            File.WriteAllText($"{sPath}\\LegoSetSql.sql", resultText);

                            IRazorEngineCompiledTemplate templateMD = razorEngine.Compile(templateTextMD);
                            var resultTextMD = templateMD.Run(model);
                            File.WriteAllText($"{sPath}\\LegoSetMD.md", resultTextMD);
                            
                        }        
                    }

                    //if (Path.GetFileName(d).Contains(".JSON"))
                    //{
                    //JSONparser irgendwas was auch immer 
                    string json = File.ReadAllText(d);
                    //}
                    //if (Path.GetFileName(d).Contains(".XML"))
                    //{

                    //}
                    //if (Path.GetFileName(d).Contains(".exel"))
                    //{

                    //}

                    // SqlCommand cmd = new SqlCommand("INSERT INTO USERS(username, email, phone) values ('" + EntityName.Text + "','" + AttributeType.Text + "','" + AttributeDataType.Text + "')", con);
                    // cmd.ExecuteNonQuery();
                    // con.Close();
                }

                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d),
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
