using System;

namespace BlazorStore.Model.Services.Catalog
{
    public static class CatalogSortExtensions
    {
        public static string AsString(this CatalogSort sort)
        {
            switch(sort)
            {
                case CatalogSort.PriceAsc: return "Price (asc)";
                case CatalogSort.PriceDesc: return "Price (desc)";
                case CatalogSort.DiscountDesc: return "Discount";
                default: throw new ArgumentOutOfRangeException(nameof(sort));
            };
        }
    }
}