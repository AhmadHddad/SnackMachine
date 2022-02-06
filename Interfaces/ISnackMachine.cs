using SnackVendingMachine.Enums;
using SnackVendingMachine.Models;

namespace SnackVendingMachine.Interfaces
{
    public interface ISnackMachine
    {
        void HandleNotes(int amountToBePurchased, ref Item itemToBePurchased);
        void HandleCoins(int amountToBePurchased, ref Item itemToBePurchased);
        void HandleCards(int amountToBePurchased, ref Item itemToBePurchased);
        List<Item> GetSnackItems();
        void DisplayItems(List<Item> items);
        Item GetItemToBePurchased(List<Item> snacksItems);
        PaymentMethod GetPaymentMethod();
        int GetAmountToBePurchased(Item itemToBePurchased);
    }
}