using System.Collections.Generic;

namespace BlazorStore.Services.Cart
{
    public class CartEventArgs
    {
        public IEnumerable<CartItem> Items { get; set; }
    }
}
