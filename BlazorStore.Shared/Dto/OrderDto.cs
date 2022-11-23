using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Shared.Dto
{
    public abstract class OrderDto
    {
        public IEnumerable<OrderLineDto> Lines { get; set; }
        public double Amount { get; set; }

        // Shipping info
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}