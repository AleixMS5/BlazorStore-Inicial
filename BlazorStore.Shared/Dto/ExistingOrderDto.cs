using System;

namespace BlazorStore.Shared.Dto
{
    public class ExistingOrderDto : OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}