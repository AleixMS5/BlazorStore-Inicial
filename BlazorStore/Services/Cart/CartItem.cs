using BlazorStore.Shared.Dto;
using System.Security.AccessControl;

namespace BlazorStore.Services.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public CartItem()
        {

        }
        public CartItem(ProductDto product, int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Image = product.Image;
            UnitPrice = product.Price;
            Quantity = quantity;                
        }
    }
}
