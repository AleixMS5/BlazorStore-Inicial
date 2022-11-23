using System;
using System.ComponentModel.DataAnnotations;
using BlazorStore.Shared.Utils;

namespace BlazorStore.Model.Data
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(FieldLenghts.User.Email)]
        public string Email { get; set; }
        [Required, MaxLength(FieldLenghts.User.Name)]
        public string Name { get; set; }
        [Required, MaxLength(FieldLenghts.User.Address)]
        public string Address { get; set; }
        [Required, MaxLength(FieldLenghts.User.City)]
        public string City { get; set; }
        [MaxLength(FieldLenghts.User.ZipCode)]
        public string ZipCode { get; set; }

        [MaxLength(FieldLenghts.User.State)]
        public string State { get; set; }
        [Required, MaxLength(FieldLenghts.User.Country)]
        public string Country { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; }
    }
}