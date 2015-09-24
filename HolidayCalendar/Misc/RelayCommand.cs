using System;
using System.Windows.Input;

namespace HolidayCalendar.Misc
{
    public class RelayCommand : ICommand
    {
        public Action<object> Action { get; set; }
        public Predicate<object> Predicate { get; set; }


        public RelayCommand(Action<object> action, Predicate<object> predicate) : this(action)
        {
            Predicate = predicate;
        }

        public RelayCommand(Action<object> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Command Action must not be null.");

            Action = action;
        }

        public bool CanExecute(object parameter)
        {
            if (Predicate == null)
                return true;

            return Predicate(parameter);
        }

        public void Execute() => Execute(null);

        public void Execute(object parameter)
        {
            Action.Invoke(parameter);
        }

        //TODO: Make that fire manually
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
    }
}
