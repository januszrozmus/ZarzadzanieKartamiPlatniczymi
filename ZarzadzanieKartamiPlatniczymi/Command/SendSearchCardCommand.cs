using System.Windows.Input;

namespace ZarzadzanieKartamiPlatniczymi.Command
{
    public class SendSearchCardCommand : ICommand
    {
        private CardsVM VM;

        public SendSearchCardCommand(CardsVM VM)
        {
            this.VM = VM;
        }

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
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var item = VM.Cards.Where(r => r.SerialNumber.Contains(VM.SearchSerialNumberString)).FirstOrDefault();
            if (item != null)
            {
                VM.CurrentCard = item;
            }

        }
    }
}
