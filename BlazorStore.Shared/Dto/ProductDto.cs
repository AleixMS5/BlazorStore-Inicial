using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Shared.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double PrevPrice { get; set; }
        public int CategoryId { get; set; }
        public bool IsHighlighted { get; set; }
    }
}
