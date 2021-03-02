using System;
using ShopsRUs.Domain.Enum;
using ShopsRUs.Domain.Infrastructure;

namespace ShopsRUs.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public UsersType UsersType
        {
            get => UserType.ParseEnum<UsersType>();
            set => UserType = value.ToString();
        }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}