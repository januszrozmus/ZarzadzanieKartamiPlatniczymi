using MgmtCardLib;
using System.Windows;
using System.Windows.Input;

namespace ZarzadzanieKartamiPlatniczymi.Command
{
    public class SendAddCardCommand : ICommand
    {
        private CardsVM VM;

        public SendAddCardCommand(CardsVM VM)
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
            if (VM.CurrentCard.SerialNumber == null) return false;
            if (VM.CurrentCard.UniqId == null) return false;
            if (VM.CurrentCard.CustomerAccountId == null) return false;
            if (VM.CurrentCard.Pin == null) return false;

            return true;
        }

        public void Execute(object parameter)
        {
            if (VM.CardContext == null) return;
            if (!Helper.CheckAddLimits(VM.CurrentCard.SerialNumber.Length, VM.CurrentCard.UniqId.Length, VM.CurrentCard.CustomerAccountId.Length, VM.CurrentCard.Pin.Length))
            {
                MessageBox.Show("Check the correctness of the data", "Check...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (VM.Cards.Where(r => r.SerialNumber == VM.CurrentCard.SerialNumber).ToList().Count > 0)
            {
                VM.CardContext.Update(VM.CurrentCard);
            }
            else
            {
                VM.CardContext.Add(VM.CurrentCard);
            }

            try 
            {
                VM.CardContext.SaveChanges();
                VM.RefreshDbData();
                VM.CurrentCard = new MgmtCardLib.Model.CustomerCard();
            }
            catch (Exception ex)
            {
                ErrorLog.SaveLog("Pressed AddButton: " + ex.Message);
                MessageBox.Show("Incorrect Data\nPlease create new card! (\"New Card button\")", "Information...", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
