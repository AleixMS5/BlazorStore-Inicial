using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace BlazorStore.Model.Services.Catalog
{
    public class SearchCriteria
    {
        public static double MIN_PRICE = 0;
        public static double MAX_PRICE = 300;

        public string Term { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; } = MAX_PRICE;
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CatalogSort Sort { get; set; } = CatalogSort.PriceAsc;

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(CategoryName) && string.IsNullOrWhiteSpace(Term) && MinPrice == MIN_PRICE && MaxPrice == MAX_PRICE)
                return "all products";

            string result;
            if (MinPrice == MaxPrice)
            {
                result = MinPrice + "€ products";
            }
            else
            {
                result = "products";
            }
                       
            if (CategoryName != null)
            {
                result += " in " + CategoryName;
            }

            if (MinPrice != MaxPrice && (MinPrice != MIN_PRICE || MaxPrice != MAX_PRICE))
            {
                if (MinPrice > MIN_PRICE && MaxPrice < MAX_PRICE)
                {
                    result += $" between {MinPrice} and {MaxPrice} €";
                }
                else if (MinPrice > MIN_PRICE)
                {
                    result += $" starting at {MinPrice} €";
                }
                else
                {
                    result += $" up to {MaxPrice} €";

                }
            }
            if (!string.IsNullOrWhiteSpace(Term))
            {
                result += $" containing the text '{Term}'";
            }
            return result;
        }
    }
}
