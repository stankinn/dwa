using System.Windows.Controls;
using System.Windows;
using Repository.Operations;

namespace WpfApp2019.View
{
    /// <summary>
    /// Interaktionslogik für AddEntityView.xaml
    /// </summary>
    public partial class AddEntityView : UserControl
    {
        public AddEntityView()
        {
            InitializeComponent();
            ComboBoxAlter.Visibility = Visibility.Hidden;
        }
        #region add
        /*
        private void AddEntity_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ae = new AddEntity();
            decimal value;

            if (decimal.TryParse(AttributeNames.Text, out value))
            {
                ae.add(EntityName.Text, value);
                MessageBox.Show(EntityName.Text + " has been added for the price of " + value + ".");

            }
            else
            {
                MessageBox.Show("Please enter a valid price.");
            }
        }
        private void AddEntityAll_Click(object sender, RoutedEventArgs e)
        {
            var ae = new AddEntity();
            decimal value;
            int cal;
            int dia;

            if (decimal.TryParse(AttributeNames.Text, out value))
            {
                if (int.TryParse(AttributeType.Text, out cal))
                {
                    if (int.TryParse(AttributeDataType.Text, out dia))
                    {
                        if (cal > 0 && dia > 0 && value > 0)
                        {
                            ae.addAll(EntityName.Text, value, cal, dia);
                            MessageBox.Show(EntityName.Text + " has been added for the price of " + value + " with " + cal + " calories and " + dia + " diameter.");
                        }
                        else if (cal < 0 || dia < 0 || value < 0)
                        {
                            MessageBox.Show("Price, calories and diameter must be positive.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for the diameter.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for the calories.");
                }


            }
            else
            {
                MessageBox.Show("Please enter a valid price.");
            }

        }

        */
        #endregion

        #region alter

        /*private void AlterEntityRename_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ae = new AlterEntity();

            ae.alterRename(EntityName.Text, AttributeNames.Text);
            MessageBox.Show(EntityName.Text + " has been renamed to " + AttributeNames.Text + ".");


        }

        private void AlterEntityChangePrice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ae = new AlterEntity();
            decimal value;

            if (decimal.TryParse(AttributeNames.Text, out value))
            {
                if (ae.doesExist(EntityName.Text) == true)
                {
                    ae.alterChangePrice(EntityName.Text, value);
                    MessageBox.Show("The price of " + EntityName.Text + " been changed to " + value + ".");
                }
                else
                {
                    MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid price.");
            }

        }
        private void AlterEntityChangeCalories_Click(object sender, RoutedEventArgs e)
        {
            var ae = new AlterEntity();

            int cal;

            if (int.TryParse(AttributeNames.Text, out cal))
            {
                if (ae.doesExist(EntityName.Text) == true)
                {

                    ae.alterChangeCalories(EntityName.Text, cal);
                    MessageBox.Show("The calories of " + EntityName.Text + " have been changed to " + cal + ".");
                }
                else
                {
                    MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
        private void AlterEntityChangeDiameter_Click(object sender, RoutedEventArgs e)
        {
            var ae = new AlterEntity();

            int dia;

            if (int.TryParse(AttributeNames.Text, out dia))
            {
                if (ae.doesExist(EntityName.Text) == true)
                {

                    ae.alterChangeDiameter(EntityName.Text, dia);
                    MessageBox.Show("The diameter of " + EntityName.Text + " has been changed to " + dia + ".");
                }
                else
                {
                    MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }*/
        #endregion
        #region delete
        /*
        private void DeleteEntity_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var de = new DeleteEntity();

            de.deleteByName(EntityName.Text);

        }

        private void DeleteEntityByPrice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var de = new DeleteEntity();
            decimal value;

            if (decimal.TryParse(AttributeNames.Text, out value))
            {
                de.deleteByPrice(value);
            }


        }

        private void DeleteEntityById_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var de = new DeleteEntity();
            int value;

            if (int.TryParse(AttributeNames.Text, out value))
            {
                de.deleteById(value);
            }
        }

        */
        #endregion


        

        private void ComboBoxSelect_Initialized(object sender, System.EventArgs e)
        {
            ComboBoxSelect.Items.Add("Add Entity");
            ComboBoxSelect.Items.Add("Alter Entity");
            ComboBoxSelect.Items.Add("Delete Entity");

        }

        private void AllButton_Click(object sender, RoutedEventArgs e)
        {
            //add entity
            if (ComboBoxSelect.SelectedIndex.Equals(0)) {
                //check if all input fields are empty
                if (EntityName.Text.Equals("") && AttributeNames.Text.Equals("") && AttributeType.Text.Equals("") && AttributeDataType.Text.Equals(""))
                {
                    MessageBox.Show("The input fields are empty.");
                }
                //check if name and price is filled
                else if (!EntityName.Text.Equals("") && !AttributeNames.Text.Equals("") && AttributeType.Text.Equals("") && AttributeDataType.Text.Equals(""))
                {
                    var ae = new AddEntity();
                    decimal value;

                    if (decimal.TryParse(AttributeNames.Text, out value))
                    {
                        ae.add(EntityName.Text, value);
                        MessageBox.Show(EntityName.Text + " has been added for the price of " + value + ".");

                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid price.");
                    }
                } else if (!EntityName.Text.Equals("") && !AttributeNames.Text.Equals(""))
                {
                    var ae = new AddEntity();
                    decimal value;
                    int cal;
                    int dia;

                    if (decimal.TryParse(AttributeNames.Text, out value))
                    {
                        if (int.TryParse(AttributeType.Text, out cal))
                        {
                            if (int.TryParse(AttributeDataType.Text, out dia))
                            {
                                if (cal > 0 && dia > 0 && value > 0)
                                {
                                    ae.addAll(EntityName.Text, value, cal, dia);
                                    MessageBox.Show(EntityName.Text + " has been added for the price of " + value + " with " + cal + " calories and " + dia + " diameter.");
                                }
                                else if (cal < 0 || dia < 0 || value < 0)
                                {
                                    MessageBox.Show("Price, calories and diameter must be positive.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid number for the diameter.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid number for the calories.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid price.");
                    }
                }

            }
            //alter entity
            else if (ComboBoxSelect.SelectedIndex.Equals(1))
            {
                #region add
                if (ComboBoxAlter.SelectedIndex.Equals(0))
                {
                    var ae = new AlterEntity();

                    ae.alterRename(EntityName.Text, AttributeNames.Text);
                    MessageBox.Show(EntityName.Text + " has been renamed to " + AttributeNames.Text + ".");
                } else if (ComboBoxAlter.SelectedIndex.Equals(1))
                {
                    var ae = new AlterEntity();
                    decimal value;

                    if (decimal.TryParse(AttributeNames.Text, out value))
                    {
                        if (ae.doesExist(EntityName.Text) == true)
                        {
                            ae.alterChangePrice(EntityName.Text, value);
                            MessageBox.Show("The price of " + EntityName.Text + " been changed to " + value + ".");
                        }
                        else
                        {
                            MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid price.");
                    }
                }
                #endregion
                #region alter
                else if (ComboBoxAlter.SelectedIndex.Equals(2))
                {
                    var ae = new AlterEntity();

                    int cal;

                    if (int.TryParse(AttributeNames.Text, out cal))
                    {
                        if (ae.doesExist(EntityName.Text) == true)
                        {

                            ae.alterChangeCalories(EntityName.Text, cal);
                            MessageBox.Show("The calories of " + EntityName.Text + " have been changed to " + cal + ".");
                        }
                        else
                        {
                            MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number.");
                    }
                } else if (ComboBoxAlter.SelectedIndex.Equals(3))
                {
                    var ae = new AlterEntity();

                    int dia;

                    if (int.TryParse(AttributeNames.Text, out dia))
                    {
                        if (ae.doesExist(EntityName.Text) == true)
                        {

                            ae.alterChangeDiameter(EntityName.Text, dia);
                            MessageBox.Show("The diameter of " + EntityName.Text + " has been changed to " + dia + ".");
                        }
                        else
                        {
                            MessageBox.Show("Entry " + EntityName.Text + " does not exist.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number.");
                    }
                }
                #endregion
                //delete entity
            }
            #region delete
            else if (ComboBoxSelect.SelectedIndex.Equals(2))
            {
                if (ComboBoxAlter.SelectedIndex == 0)
                {
                    var de = new DeleteEntity();
                    if (de.doesExist(EntityName.Text) == true)
                    {
                        de.deleteByName(EntityName.Text);
                        MessageBox.Show("The entry with the name " + EntityName.Text + " has been deleted.");
                    }
                    else
                    {
                        MessageBox.Show("No entry with this name has been found.");
                    }

                }
                else if (ComboBoxAlter.SelectedIndex == 1)
                {
                    var de = new DeleteEntity();
                    decimal value;

                    if (decimal.TryParse(AttributeNames.Text, out value))
                    {
                        de.deleteByPrice(value);
                        MessageBox.Show("The entry with the price of " + value + " has been deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid price.");
                    }

                } else if (ComboBoxAlter.SelectedIndex == 2)
                {
                    var de = new DeleteEntity();
                    int value;

                    if (int.TryParse(AttributeNames.Text, out value))
                    {
                        de.deleteById(value);
                        MessageBox.Show("The entry with the ID of " + value + " has been deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid ID.");
                    }
                }
            }
                #endregion
            
        }

        private void ComboBoxSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EntityName.Text = "";
            AttributeNames.Text = "";
            AttributeType.Text = "";
            AttributeDataType.Text = "";
            if (ComboBoxSelect.SelectedIndex == 0)
            {
                EntityName.Placeholder = "Name";
                AttributeNames.Placeholder = "Price";
                AttributeType.Placeholder = "Calories";
                AttributeDataType.Placeholder = "Diameter";
                AttributeNames.Visibility = Visibility.Visible;
                AttributeType.Visibility = Visibility.Visible;
                AttributeDataType.Visibility = Visibility.Visible;
                ComboBoxAlter.Visibility = Visibility.Hidden;

            }
            else if(ComboBoxSelect.SelectedIndex == 1)
            {
                ComboBoxAlter.Items.Clear();
                ComboBoxAlter.Items.Add("Change Name");
                ComboBoxAlter.Items.Add("Change Price");
                ComboBoxAlter.Items.Add("Change Calories");
                ComboBoxAlter.Items.Add("Change Diameter");
                EntityName.Placeholder = "";
                AttributeNames.Placeholder = "";
                ComboBoxAlter.Visibility = Visibility.Visible;
                AttributeType.Visibility = Visibility.Hidden;
                AttributeDataType.Visibility = Visibility.Hidden;

            }else if(ComboBoxSelect.SelectedIndex == 2)
            {
                ComboBoxAlter.Items.Clear();
                ComboBoxAlter.Items.Add("Delete by Name");
                ComboBoxAlter.Items.Add("Delete by Price");
                ComboBoxAlter.Items.Add("Delete by ID");
                ComboBoxAlter.Visibility = Visibility.Visible;
                EntityName.Placeholder = "";
                AttributeNames.Visibility = Visibility.Hidden;
                AttributeType.Visibility = Visibility.Hidden;
                AttributeDataType.Visibility = Visibility.Hidden;
            }
        }




        private void ComboBoxAlter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ComboBoxSelect.SelectedIndex == 1)
            {
                EntityName.Text = "";
                AttributeNames.Text = "";
                EntityName.Placeholder = "Name of Product";
                if (ComboBoxAlter.SelectedIndex == 0)
                {

                    AttributeNames.Placeholder = "New Name";

                }
                else if (ComboBoxAlter.SelectedIndex == 1)
                {

                    AttributeNames.Placeholder = "New Price";

                }
                else if (ComboBoxAlter.SelectedIndex == 2)
                {

                    AttributeNames.Placeholder = "New Calories";

                }
                else if (ComboBoxAlter.SelectedIndex == 3)
                {

                    AttributeNames.Placeholder = "New Diameter";

                }
            }else if (ComboBoxSelect.SelectedIndex == 2)
            {
                EntityName.Text = "";
                if (ComboBoxAlter.SelectedIndex == 0)
                {
                    EntityName.Placeholder = "Enter Name for Deletion";
                }else if(ComboBoxAlter.SelectedIndex == 1)
                {
                    EntityName.Placeholder = "Enter Price for Deletion";
                }else if (ComboBoxAlter.SelectedIndex == 2)
                {
                    EntityName.Placeholder = "Enter ID for Deletion";
                }
            }
        }
    }
}
