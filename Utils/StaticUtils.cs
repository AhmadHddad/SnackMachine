namespace SnackVendingMachine.Utils
{
    public static class StaticUtils
    {
    
        public static decimal ParseDecimal(string stringNumber)
        {
            decimal number;

            decimal.TryParse(stringNumber, out number);


            return number;
        }

        public static int ParseInt(string stringNumber)
        {
            int number;

            int.TryParse(stringNumber, out number);


            return number;
        }

        public static decimal GetTotalPrice(decimal price, int amountToBePurchased) => decimal.Multiply(price, (decimal)amountToBePurchased);

        public static decimal ReadDecimal() => StaticUtils.ParseDecimal(Console.ReadLine()!);

        public static int ReadInt() => StaticUtils.ParseInt(Console.ReadLine()!);

        public static string RepeateString(string stringToReapete, int timesTorepete)
        {
            string st = "";


            for (int i = 0; i < timesTorepete; i++)
            {
                st += $" {stringToReapete}";
            }

            return st;
        }


        public static IEnumerable<T> GetEnumValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();

        public static string StringifyMoney(decimal money)
        {
            string moneyMessage;

            if (money >= 100)
            {
                moneyMessage = $"{money / 100}$";
            }
            else
            {
                moneyMessage = $"{money}c";

            }

            return moneyMessage;
        }

    }
}