using MgmtCardLib.Interfaces;
using MgmtCardLib.Tools;
using System.ComponentModel.DataAnnotations;

namespace MgmtCardLib.Model
{
    public class CustomerCard : ObservedObject, IUniqID, ICreateCard
    {
        private string _CustomerAccountId = string.Empty;
        [StringLength(26)]
        public string CustomerAccountId
        {
            get
            {
                return _CustomerAccountId;
            }
            set
            {

                _CustomerAccountId = value;
                OnPropertyChanged(nameof(CustomerAccountId));
            }
        }

        private string _Pin = string.Empty;
        [StringLength(4)]
        public string Pin
        {
            get
            {
                return _Pin;
            }
            set
            {

                _Pin = value;
                OnPropertyChanged(nameof(Pin));
            }
        }

        private string _SerialNumber = string.Empty;
        [Key]
        [StringLength(13)]
        public string SerialNumber
        {
            get
            {
                return _SerialNumber;
            }
            set
            {

                _SerialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
                if (string.IsNullOrEmpty(UniqId) && SerialNumber.Length == Helper.CardSnLim)
                {
                    SetUniqId(Helper.GenerateIdentifier());
                }
            }
        }

        private string _UniqId { get; set; } = string.Empty;
        [StringLength(32)]
        public string UniqId
        {
            get
            {
                return _UniqId;
            }
            set
            {

                _UniqId = value;
                OnPropertyChanged(nameof(UniqId));
            }
        }

        public void SetUniqId(string uniqId)
        {
            UniqId = uniqId;
        }

        public string GetSerialNumber()
        {
            return SerialNumber;
        }

        public string GetUniqID()
        {
            return UniqId;
        }
    }
}
