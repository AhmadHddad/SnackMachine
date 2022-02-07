using Newtonsoft.Json;
using SnackVendingMachine.Models;
using SnackVendingMachine.Utils;
using SnackVendingMachine.Enums;
using SnackVendingMachine.Interfaces;

namespace SnackVendingMachine.Classes
{
    public class SnackMachine : ISnackMachine
    {
        public void HandleCoins(int amountToBePurchased, ref Item itemToBePurchased)
        {
            var totalPrice = StaticUtils.GetTotalPrice(itemToBePurchased.Price, amountToBePurchased);

            var insertedMoney = InsertMoney(PaymentMethod.Coins, 0, totalPrice);

            if (totalPrice > insertedMoney)
            {
                System.Console.WriteLine($"{insertedMoney} is not enough, you need {totalPrice - insertedMoney} more!");
                Console.WriteLine("Please press enter");
            }
            else
            {
                PostPurchaseAction(insertedMoney, totalPrice, amountToBePurchased, ref itemToBePurchased);
            }
        }

        public void HandleNotes(int amountToBePurchased, ref Item itemToBePurchased)
        {
            var totalPrice = StaticUtils.GetTotalPrice(itemToBePurchased.Price, amountToBePurchased);

            var insertedMoney = InsertMoney(PaymentMethod.Notes, 0, totalPrice);

            if (totalPrice > insertedMoney)
            {
                System.Console.WriteLine($"{insertedMoney} is not enough, you need {totalPrice - insertedMoney} more!");
                Console.WriteLine("Please press enter");
            }
            else
            {
                PostPurchaseAction(insertedMoney, totalPrice, amountToBePurchased, ref itemToBePurchased);
            }
        }

        public void HandleCards(int amountToBePurchased, ref Item itemToBePurchased)
        {
            var totalPrice = StaticUtils.GetTotalPrice(itemToBePurchased.Price, amountToBePurchased);
            System.Console.WriteLine("Processing ...");
            Thread.Sleep(2000);
            System.Console.WriteLine($"{StaticUtils.StringifyMoney(totalPrice)} payed successfully!");

            PostPurchaseAction(totalPrice, totalPrice, amountToBePurchased, ref itemToBePurchased);

        }

        private void PostPurchaseAction(decimal insertedMoney, decimal totalPrice, int amountToBePurchased, ref Item itemToBePurchased)
        {
            var change = insertedMoney - totalPrice;
            var itemEmoji = itemToBePurchased.Name.Split(" ")[1];
            if (change > 0)
            {
                System.Console.WriteLine($"Change is {StaticUtils.StringifyMoney(insertedMoney - totalPrice)}");
            }
            System.Console.WriteLine($"Here {StaticUtils.RepeateString(itemEmoji, amountToBePurchased)}");
            System.Console.WriteLine("Thanks");
            itemToBePurchased.AvailableItemsCount -= amountToBePurchased;

            System.Console.WriteLine("Please press enter.");
        }

        public List<Item> GetSnackItems()
        {

            var stringObj = @"[{'id':1,'price':10,'name':'Candy 🍭','availableItemsCount':5},{'id':2,'price':20,'name':'Gum 🍬','availableItemsCount':5},{'id':3,'price':50,'name':'Chocklet 🍫','availableItemsCount':5},{'id':4,'price':100,'name':'Coca Cola 🥤','availableItemsCount':5},{'id':5,'price':200,'name':'Dotantes 🍩','availableItemsCount':5}]";
            return JsonConvert.DeserializeObject<List<Item>>(stringObj)!;

        }

        private List<decimal> GetAllwedCoins()
        {
            return new List<decimal> { 10, 20, 50, 100 };
        }
        private List<decimal> GetAllwedNotes()
        {
            return new List<decimal> { 2000, 5000 };
        }
        private decimal ParseMoney(string insertedMoney)
        {
            decimal money = 0;

            decimal digets = 0;
            try
            {
                digets = Convert.ToDecimal(insertedMoney.Substring(0, insertedMoney.Length - 1));
            }
            catch (System.Exception)
            {
            }

            var sign = insertedMoney.Last().ToString();

            if (new List<string> { "$", "c", "C" }.Any(s => s == sign))
            {

                if (sign == "$")
                {
                    money += digets * 100;
                }
                else
                {
                    money += digets;
                }
            }

            return money;
        }

        private bool IsValideMoney(PaymentMethod paymentMehtod, decimal insertedMoney)
        {
            var allowedMoney = PaymentMethod.Coins == paymentMehtod ? this.GetAllwedCoins() : this.GetAllwedNotes();
            return allowedMoney.Any(x => x == insertedMoney);
        }

        private bool WantToInsertMore()
        {
            System.Console.WriteLine("Do you want to insret more?");
            System.Console.WriteLine("1] Yes");
            System.Console.WriteLine("2] No");

            return Console.ReadLine() == "1";

        }

        private decimal InsertMoney(PaymentMethod paymentMethod, decimal insertedMoney = 0, decimal totalPrice = 0)
        {
            string message = "Please insert ";

            if (paymentMethod == PaymentMethod.Notes)
            {
                message += "Notes: 20$ or 50$";
            }
            else
            {
                message += "Coins: 10c, 20c, 50c or 1$";

            }

            Console.WriteLine(message);

            decimal totalInsertedCoins = insertedMoney;
            var insertedCoins = ParseMoney(Console.ReadLine()!);
            var allowedMoney = PaymentMethod.Coins == paymentMethod ? this.GetAllwedCoins() : this.GetAllwedNotes();

            if (IsValideMoney(paymentMethod, insertedCoins))
            {
                totalInsertedCoins += insertedCoins;
                System.Console.WriteLine($"Total Inserted Money: {StaticUtils.StringifyMoney(totalInsertedCoins)}, Total Price: {StaticUtils.StringifyMoney(totalPrice)}");

                if(totalInsertedCoins >= totalPrice) return totalInsertedCoins;


                if (WantToInsertMore())
                {
                    return InsertMoney(paymentMethod, totalInsertedCoins, totalPrice);
                }
                else
                {
                    if (totalPrice != 0 && totalInsertedCoins < totalPrice)
                    {
                        System.Console.WriteLine("You need more money");

                        if (WantToInsertMore())
                        {
                            return InsertMoney(paymentMethod, totalInsertedCoins, totalPrice);
                        }
                        else
                        {
                            return totalInsertedCoins;
                        }
                    }

                    return totalInsertedCoins;
                }

            }
            else
            {
                System.Console.WriteLine("Not valid money, please try again");
                return InsertMoney(paymentMethod, totalInsertedCoins, totalPrice);
            }
        }

        private void DispalyMoney(decimal money)
        {
            string moneyMessage;

            if (money >= 100)
            {
                moneyMessage = $"{money / 100}$";
            }
            else
            {
                moneyMessage = $"{money / 100}c";

            }

            System.Console.WriteLine(moneyMessage);
        }

        public void DisplayItems(List<Item> items)
        {

            Console.WriteLine($"\n\n{"#".PadRight(5)} {"Stock"} { "Product".PadRight(47) } { "Price".PadLeft(7)}");
            foreach (var item in items)
            {
                if (item.IsAvailable)
                {
                    string itemNumber = item.Id.ToString().PadRight(5);
                    string itemsRemaining = item.AvailableItemsCount.ToString().PadRight(5);
                    string productName = item.Name.PadRight(40);
                    string price = StaticUtils.StringifyMoney(item.Price).PadLeft(7);
                    Console.WriteLine($"{itemNumber} {itemsRemaining} {productName} Price: {price} each");
                }
                else
                {
                    Console.WriteLine($"{item.Id}: {item.Name} IS SOLD OUT.");
                }
            }
        }

        public Item GetItemToBePurchased(List<Item> snacksItems)
        {
            var itemId = StaticUtils.ReadInt();

            var itemToBePurchased = snacksItems.FirstOrDefault(i => i.Id == itemId);

            if (itemToBePurchased != null)
            {
                if (itemToBePurchased.AvailableItemsCount == 0)
                {
                    System.Console.WriteLine("Out Of Stock, Please choose another item, or try later");
                    System.Console.WriteLine(" ");
                    System.Console.WriteLine("Please choose available item, or press enter");
                    return GetItemToBePurchased(snacksItems);
                }
                else
                {

                    return itemToBePurchased;
                }
            }
            else
            {
                System.Console.WriteLine("Item does not exist, Please enter a valid item #");
                this.DisplayItems(snacksItems);
                return GetItemToBePurchased(snacksItems);
            }

        }
        public PaymentMethod GetPaymentMethod()
        {
            Console.WriteLine("Payment Method: How do you want to pay?");
            Console.WriteLine("1] Coins: We accept 10c, 20c, 50c and 1$");
            Console.WriteLine("2] Card: We accept all cards");
            Console.WriteLine("3] Notes: We accept 20$ and 50$ only");


            var enteredMethod = StaticUtils.ReadInt();

            var paymentMethodsList = StaticUtils.GetEnumValues<PaymentMethod>();

            if (paymentMethodsList.Any(x => (int)x == enteredMethod))
            {
                return (PaymentMethod)enteredMethod;
            }
            else
            {
                Console.WriteLine("Invalid payment method");
                Thread.Sleep(1000);
                Console.WriteLine("");
                return GetPaymentMethod();
            }
        }

        public int GetAmountToBePurchased(Item itemToBePurchased)
        {
            Console.WriteLine($"How much do you want to purchase of {itemToBePurchased.Name} ({itemToBePurchased.AvailableItemsCount})?");

            var amountToBePurchased = StaticUtils.ReadInt();

            if (itemToBePurchased.AvailableItemsCount >= amountToBePurchased)
            {
                return amountToBePurchased;
            }
            else
            {
                System.Console.WriteLine("We can't provide that much");
                System.Console.WriteLine(" ");
                return GetAmountToBePurchased(itemToBePurchased);
            }
        }

        // --TESTING ONLY!--
        public bool IsValideMoneyTest(PaymentMethod paymentMethod, decimal insertedMoney) => this.IsValideMoney(paymentMethod, insertedMoney);
        public decimal ParseMoneyTest(string insertedMoney) => this.ParseMoney(insertedMoney);
    }
}
