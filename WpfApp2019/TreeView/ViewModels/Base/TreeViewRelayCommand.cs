using System;
using System.Windows.Input;

namespace WpfApp2019.TreeView
{
    // A basic command that runs an Action
    public class TreeViewRelayCommand : ICommand
    {
        #region Private Members

        // The action to run
        private Action mAction;

        #endregion

        #region Public Events

        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        public TreeViewRelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods

        // A relay command can always execute
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Executes the commands Action
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
