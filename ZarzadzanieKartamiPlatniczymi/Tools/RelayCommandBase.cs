using System.Windows.Input;

namespace ZarzadzanieKartamiPlatniczymi.Tools
{
    public class RelayCommandBase
    {

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}