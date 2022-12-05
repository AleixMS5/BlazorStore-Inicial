using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorStore.Extensions.DataAnnotations;
using BlazorStore.Shared.Dto;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Pages.Admin.Products
{
    public class ProductViewModel: IValidatableObject
    {
        [RequiredField, FieldLength(FieldLenghts.Product.Name)]
        public string Name { get; set; }
        public string Description { get; set; }
        [RequiredField, FieldLength(FieldLenghts.Product.Description)]
        public string Image { get; set; }
        public double Price { get; set; }
        public double PrevPrice { get; set; }
        public bool IsHighlighted { get; set; }
        [RequiredField]
        public int? CategoryId { get; set; }

        public static ProductViewModel FromProductDto(ProductDto dto)
        {
            return new ProductViewModel()
            {
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                PrevPrice = dto.PrevPrice,
                IsHighlighted = dto.IsHighlighted
            };
        }

        public ProductDto ToProductDto()
        {
            return new ProductDto()
            {
                Name = Name,
                Description = Description,
                Image = Image,
                CategoryId = CategoryId.GetValueOrDefault(),
                Price = Price,
                PrevPrice = PrevPrice,
                IsHighlighted = IsHighlighted
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PrevPrice < Price)
            {
                yield return new ValidationResult("Previous price must be equal or greater than Price", new [] { nameof(PrevPrice) });
            }
        }
    }
}
