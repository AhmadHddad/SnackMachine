using Xunit;
using SnackVendingMachine.Classes;
using SnackVendingMachine.Enums;

namespace SnackVendingMachine.Testing
{
    public class SnackMachineTest
    {
        private readonly SnackMachine _snackMachine;

        public SnackMachineTest()
        {
            _snackMachine = new SnackMachine();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(100)]
        public void ParseCoinMoneyTestTheory(decimal insertedMoney)
        {
            Assert.True(_snackMachine.IsValideMoneyTest(Enums.PaymentMethod.Coins, insertedMoney));
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(5000)]
        public void ParseNoteMoneyTestTheory(decimal insertedMoney)
        {
            Assert.True(_snackMachine.IsValideMoneyTest(Enums.PaymentMethod.Cards, insertedMoney));
        }


        [Theory]
        [InlineData("notValidMoney", 0)]
        [InlineData("10$", 1000)]
        [InlineData("10c", 10)]
        [InlineData("20C", 20)]
        public void ParseMoneyTestTheory(string insertedMoney, decimal expectedRes)
        {
            Assert.Equal(_snackMachine.ParseMoneyTest(insertedMoney), expectedRes);
        }


        [Theory]
        [InlineData(PaymentMethod.Coins, 10, true)]
        [InlineData(PaymentMethod.Coins, 20, true)]
        [InlineData(PaymentMethod.Coins, 50, true)]
        [InlineData(PaymentMethod.Coins, 100, true)]
        [InlineData(PaymentMethod.Coins, 30, false)]

        public void IsValideCoinTestTheory(PaymentMethod paymentMethod, decimal money, bool res)
        {
            Assert.Equal(_snackMachine.IsValideMoneyTest(paymentMethod, money), res);
        }

        [Theory]
        [InlineData(PaymentMethod.Notes, 2000, true)]
        [InlineData(PaymentMethod.Notes, 5000, true)]
        [InlineData(PaymentMethod.Notes, 100, false)]
        public void IsValideNotesTheory(PaymentMethod paymentMethod, decimal money, bool res)
        {
            Assert.Equal(_snackMachine.IsValideMoneyTest(paymentMethod, money), res);
        }


    }
}