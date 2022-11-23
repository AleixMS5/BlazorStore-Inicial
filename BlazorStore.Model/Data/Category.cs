using System.ComponentModel.DataAnnotations;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Model.Data
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(FieldLenghts.Category.Name)]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
