using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_LIB
{
    public class RelayCommandHandlerObject : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Initializes a new instance of the RelayCommandHandlerObject class. Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked.</param>
        public RelayCommandHandlerObject(Action execute)
        : this(_ => execute())
        {
        }
        /// <summary>
        /// Initializes a new instance of the RelayCommandHandlerObject class.
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked.</param>
        /// <param name="canExecute">The function to determine if the command can execute.</param>

        public RelayCommandHandlerObject(Action execute, Func<bool> canExecute)
            : this(_ => execute(), _ => canExecute())
        {
        }
        /// <summary>
        /// Initializes a new instance of the RelayCommandHandlerObject class. Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked.</param>

        public RelayCommandHandlerObject(Action<object> execute)
            : this(execute, null)
        {
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommandHandlerObject(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;

            /* OLD WAY
                      if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
            */
        }
        /// <summary>
        /// Determines whether this RelayCommand can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
            //OLD:  return _canExecute == null ? true : _canExecute();
        }
        /// <summary>
        /// Executes the RelayCommand on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        /// <summary>
        /// Method used to raise the CanExecuteChanged event
        /// to indicate that the return value of the CanExecute
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            /*            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
             */
        }
    }

}
