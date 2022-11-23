using System;

namespace BlazorStore.Shared.Dto
{
    public class UserDto: UserProfileDto
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationDate { get; set; }
    }
}