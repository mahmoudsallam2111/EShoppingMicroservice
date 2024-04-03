namespace Basket.Api.Models
{
    public class ShoppingCartItem
    {
        public int Quentity { get; set; } = default;

        public string Color = default!;
        public decimal Price { get; set; }
        public Guid Productid { get; set; }
        public string ProductName { get; set; } = default!;
    }
}
