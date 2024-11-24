using MgmtCardLib;
using MgmtCardLib.Model;
using MgmtCardLib.Tools;
using System.Data;
using System.Windows;
using System.Windows.Input;
using ZarzadzanieKartamiPlatniczymi.Command;

namespace ZarzadzanieKartamiPlatniczymi
{
    public class CardsVM : ObservedObject
    {
        public ICommand CloseCommand { get; }

        private CustomerCardDbContext? _CardContext = null;
        public CustomerCardDbContext? CardContext 
        { 
            get
            {
                if (_CardContext == null) _CardContext = new CustomerCardDbContext();
                return _CardContext;
            }
            set
            {
                _CardContext = value;
                OnPropertyChanged(nameof(CardContext));
            }
        }

        public bool IsUpdate { get; set; } = false;

        public void RefreshDbData()
        {
            Cards = CardContext.CustomerCards.ToList();
        }

        private List<CustomerCard>? _Cards;
        public List<CustomerCard> Cards
        {
            get
            {
                try
                {
                    if (_Cards == null) _Cards = CardContext.CustomerCards.ToList();
                    return _Cards;
                }
                catch (Exception ex)
                {
                    ErrorLog.SaveLog("Get Cards value: " + ex.Message);
                    MessageBoxResult msgRes = MessageBox.Show("Database connection ERROR - EntityFrameworkCore\nApplication will be closed", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (msgRes == MessageBoxResult.OK)
                    {
                        ApplicationClose();
                    }
                    return new List<CustomerCard>();    
                }
            }
            set
            {
                _Cards = value; 
                OnPropertyChanged(nameof(Cards));
            }
        }

        public event EventHandler CloseRequested;
        private void ApplicationClose()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private string _SearchSerialNumberString = string.Empty;
        public string SearchSerialNumberString
        {
            get
            {
                return _SearchSerialNumberString;
            }
            set
            { 
                _SearchSerialNumberString = value;
                OnPropertyChanged(nameof(SearchSerialNumberString));
            }
        }

        private CustomerCard _CurrentCard = new CustomerCard();
        public CustomerCard CurrentCard
        {
            get
            {
                if (_CurrentCard == null) _CurrentCard = new CustomerCard();
                return _CurrentCard;
            }
            set
            {
                _CurrentCard = value;
                OnPropertyChanged(nameof(CurrentCard));
            }
        }

        // Button commands
        private ICommand? _SendNewCard = null;
        public ICommand SendNewCard
        {
            get
            {
                if (_SendNewCard == null) _SendNewCard = new SendNewCardCommand(this);
                return _SendNewCard;
            }
        }

        private ICommand? _SendAddCard = null;
        public ICommand SendAddCard
        {
            get
            {
                if (_SendAddCard == null) _SendAddCard = new SendAddCardCommand(this);
                return _SendAddCard;
            }
        }

        private ICommand? _SendRemoveCard = null;
        public ICommand SendRemoveCard
        {
            get
            {
                if (_SendRemoveCard == null) _SendRemoveCard = new SendRemoveCardCommand(this);
                return _SendRemoveCard;
            }
        }

        private ICommand? _SendSearchCard = null;
        public ICommand SendSearchCard
        {
            get
            {
                if (_SendSearchCard == null) _SendSearchCard = new SendSearchCardCommand(this);
                return _SendSearchCard;
            }
        }

        private ICommand? _SendGenerateUniqId = null;
        public ICommand SendGenerateUniqId
        {
            get
            {
                if (_SendGenerateUniqId == null) _SendGenerateUniqId = new SendGenerateUniqIdCommand(this);
                return _SendGenerateUniqId;
            }
        }
    }
}
