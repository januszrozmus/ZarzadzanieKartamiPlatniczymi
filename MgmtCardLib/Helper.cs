namespace MgmtCardLib
{
    public static class Helper
    {
        private static readonly Random _Random = new Random();
        public static int CardSnLim { get { return 13; } }
        public static int UniqIdLim { get { return 32; } }
        public static int CustIdLim { get { return 26; } }
        public static int PinLim { get { return 4; } }

        public static string GenerateIdentifier(int length = 32)
        {
            const string availableCharsRange = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            var res = new char[length];
            
            for (int i = 0; i < length; i++)
            {
                res[i] = availableCharsRange[_Random.Next(availableCharsRange.Length)];
            }
            return new string(res);
        }

        public static bool CheckAddLimits(int? cardSN, int? uniqId, int? custId, int? pin)
        {
            if (cardSN == null || uniqId == null || custId ==null || pin == null) return false;
            
            return cardSN == CardSnLim && uniqId == UniqIdLim && custId == CustIdLim && pin == PinLim;
        }
    }
}
