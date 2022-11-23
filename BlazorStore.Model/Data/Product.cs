using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Model.Data
{
    public class Product
    {
        public Product()
        {

        }
        internal Product(int id, Category category, string name, string description, double price, string image, bool isHighlighted = false, double discount = 0)
        {
            Id = id;
            CategoryId = category.Id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            IsHighlighted = isHighlighted;
            LastUpdated = DateTime.Now;
            PrevPrice = Math.Round(Price + Price * discount / 100, 2);
        }

        public int Id { get; set; }
        [Required, MaxLength(FieldLenghts.Product.Name)]
        public string Name { get; set; }
        [MaxLength(FieldLenghts.Product.Description)]
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double PrevPrice { get; set; }
        public bool IsHighlighted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
