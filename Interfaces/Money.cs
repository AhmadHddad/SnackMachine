namespace SnackVendingMachine.Interfaces
{
    public interface Money
    {
        public string Currency { get; }

        public decimal Amount { get; set; }

        public string Symbol { get; }

    }
}