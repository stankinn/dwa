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
        }
        #region add
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

        #endregion

        #region alter

        private void AlterEntityRename_Click(object sender, System.Windows.RoutedEventArgs e)
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
        }
        #endregion
        #region delete
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

        #endregion


    }
}
