using System.Windows.Input;

namespace ZarzadzanieKartamiPlatniczymi.Command
{
    public class SendRemoveCardCommand : ICommand
    {
        private CardsVM VM;

        public SendRemoveCardCommand(CardsVM VM)
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
            return VM.Cards.Where(r => r == VM.CurrentCard).Count() > 0;
        }

        public void Execute(object parameter)
        {
            VM.CardContext.Remove(VM.CurrentCard);
            VM.CardContext.SaveChanges();
            VM.RefreshDbData();
        }
    }
}
