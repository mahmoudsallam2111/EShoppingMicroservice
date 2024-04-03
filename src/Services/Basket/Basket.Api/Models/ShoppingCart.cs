namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice  => Items.Sum(i=>i.Price * i.Quentity);
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public ShoppingCart()
        {
            
        }

    }
}
