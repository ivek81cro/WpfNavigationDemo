using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfNavigationDemo.Commands
{
    public sealed class RelayCommand : ICommand
    {
        //Standard ICommand event. UI listens to this to know when to re-query CanExecute. Nullable because the event may have no subscribers.
        //ICommand implementation that relays execution to delegates.
        public event EventHandler? CanExecuteChanged;

        //Delegate that contains the code to run when the command is executed. Accepts an optional parameter.
        private readonly Action<object?> _execute;

        //Optional predicate delegate used to determine whether the command can execute for a given parameter.
        //Nullable: if null, the command is always enabled.
        private readonly Func<object?, bool>? _canExecute;

        //Constructor that requires an execute delegate (throws ArgumentNullException if null) and accepts an optional canExecute delegate.
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        //Returns the result of _canExecute(parameter) if the predicate exists; otherwise returns true.
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        //Calls the _execute delegate with the provided parameter.
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        //Helper method that raises CanExecuteChanged (using the null-conditional invoke).
        //Call this when the conditions that affect CanExecute change so the UI updates.
        public void RaiseCanExecuteChanged() 
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
//The class is sealed to prevent inheritance.
//This is a common MVVM pattern: view binds buttons/menus to a RelayCommand instance on the view model so logic is kept out of the view code-behind.
