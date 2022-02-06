using SnackVendingMachine.Classes;
using SnackVendingMachine.Utils;
using SnackVendingMachine.Enums;

namespace SnackVendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to my vending machine");

            var snackMachine = new SnackMachine();
            var snacksItems = snackMachine.GetSnackItems();

            // keeping program running
            while (true)
            {
                Console.WriteLine("Main Menu");
                System.Console.WriteLine(" ");
                snackMachine.DisplayItems(snacksItems);
                System.Console.WriteLine("Please Type Item's #");

                var itemToBePurchased = snackMachine.GetItemToBePurchased(snacksItems);
                var amountToBePurchased = snackMachine.GetAmountToBePurchased(itemToBePurchased);

                Console.WriteLine(itemToBePurchased.Name);

                var totalPrice = decimal.Multiply(itemToBePurchased.Price!, (decimal)amountToBePurchased);

                System.Console.WriteLine($"That would be {StaticUtils.StringifyMoney(totalPrice)}");
                System.Console.WriteLine(" ");

                var paymentMehtod = snackMachine.GetPaymentMethod();

                switch (paymentMehtod)
                {
                    case PaymentMethod.Coins:

                        snackMachine.HandleCoins(amountToBePurchased, ref itemToBePurchased);

                        break;
                    case PaymentMethod.Cards:
                        snackMachine.HandleCards(amountToBePurchased, ref itemToBePurchased);

                        break;
                    case PaymentMethod.Notes:
                        snackMachine.HandleNotes(amountToBePurchased, ref itemToBePurchased);

                        break;
                    default:
                        Console.WriteLine("Invalid payment method");
                        break;
                }


                Console.ReadLine();
                Console.Clear();
            }


        }
    }

}
