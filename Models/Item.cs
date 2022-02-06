
namespace SnackVendingMachine.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int AvailableItemsCount { get; set; } = default!;

        public bool IsAvailable => this.AvailableItemsCount != 0;
    }
}