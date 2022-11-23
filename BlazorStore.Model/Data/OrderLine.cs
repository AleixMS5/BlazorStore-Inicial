using System.ComponentModel.DataAnnotations;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Model.Data
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [MaxLength(FieldLenghts.Product.Name)]
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}