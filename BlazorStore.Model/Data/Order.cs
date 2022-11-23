using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Model.Data
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderLine> Lines { get; set; }
        public double Amount { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Submitted;

        // Shipping info
        [Required, MaxLength(FieldLenghts.User.Name)]
        public string Name { get; set; }
        [Required, MaxLength(FieldLenghts.User.Address)]
        public string Address { get; set; }
        [Required, MaxLength(FieldLenghts.User.City)]
        public string City { get; set; }
        [Required, MaxLength(FieldLenghts.User.State)]
        public string State { get; set; }
        [MaxLength(FieldLenghts.User.Country)]
        public string ZipCode { get; set; }
        [Required, MaxLength(FieldLenghts.User.Country)]
        public string Country { get; set; }
        [Required, MaxLength(FieldLenghts.User.Email)]
        public string Email { get; set; }

        // Payment info
        [Required, MaxLength(FieldLenghts.Payments.NameOnCard)]
        public string NameOnCard { get; set; }
        [Required, CreditCard, MaxLength(FieldLenghts.Payments.CardNumber)]
        public string CardNumber { get; set; }
        public int ValidUntilMonth { get; set; }
        public int ValidUntilYear { get; set; }
        [MaxLength(FieldLenghts.Payments.CvcCode)]
        public string CvcCode { get; set; }

    }

    public enum OrderStatus
    {
        Submitted,
        Processing,
        OnTheWay,
        Delivered,
    }
}