using MgmtCardLib;
using System.Windows.Input;

namespace ZarzadzanieKartamiPlatniczymi.Command
{
    public class SendGenerateUniqIdCommand : ICommand
    {
        private CardsVM VM;

        public SendGenerateUniqIdCommand(CardsVM VM)
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
            VM.CurrentCard.SetUniqId(Helper.GenerateIdentifier());
        }
    }
}
