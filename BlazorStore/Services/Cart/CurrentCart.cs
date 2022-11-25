using Blazored.SessionStorage;
using BlazorStore.Shared.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStore.Services.Cart
{
    public class CurrentCart
    {
        private Dictionary<int, CartItem> _items = new Dictionary<int, CartItem>();
        private readonly ISessionStorageService _sessionStorageService;

        public delegate void CartEventHandler(object sender, CartEventArgs args);
        public event CartEventHandler CartUpdated;

        public CurrentCart(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public CartItem[] Items => _items.Values.ToArray();

        public async Task LoadAsync()
        {
            var items = await _sessionStorageService.GetItemAsync<CartItem[]>("cart");
            _items = items?.ToDictionary(c => c.ProductId, c => c) ?? _items;
            NotifyCartChanged();
        }

        public void Initialize(CartItem[] items)
        {
            if (items != null)
            {
                _items.Clear();
                foreach (var item in items)
                {
                    AddOrUpdateItem(item);
                }
                NotifyCartChanged();
            }
        }

        public async Task SetItemQuantityAsync(int productId, int quantity)
        {
            if (_items.ContainsKey(productId))
            {
                _items[productId].Quantity = quantity;

                await _sessionStorageService.SetItemAsync("cart", _items.Values);
                NotifyCartChanged();
            }
        }

        public async Task AddItemAsync(ProductDto product, int quantity)
        {
            var item = new CartItem(product, quantity);
            AddOrUpdateItem(item);
            await _sessionStorageService.SetItemAsync("cart", _items.Values);
            NotifyCartChanged();
        }

        public async Task RemoveItemAsync(int productId)
        {
            if (_items.ContainsKey(productId))
            {
                _items.Remove(productId);
                await _sessionStorageService.SetItemAsync("cart", _items.Values);
                NotifyCartChanged();
            }
        }

        public async Task RemoveAllAsync()
        {
            _items.Clear();
            await _sessionStorageService.SetItemAsync("cart", _items.Values);
            NotifyCartChanged();
        }

        private void NotifyCartChanged()
        {
            var args = new CartEventArgs { Items = _items.Values };
            CartUpdated?.Invoke(this, args);
        }

        private void AddOrUpdateItem(CartItem item)
        {
            if (_items.ContainsKey(item.ProductId))
            {
                _items[item.ProductId].Quantity += item.Quantity;
            }
            else
            {
                _items[item.ProductId] = item;
            }
        }
    }
}
