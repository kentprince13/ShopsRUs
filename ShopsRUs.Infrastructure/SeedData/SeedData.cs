using System;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Domain.Enum;

namespace ShopsRUs.Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {   
            //Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "William 4",
                    Address = "kfc road 4",
                    CreatedOn = new DateTime(2018, 10, 10),
                    Email = "mckensie4@gmail.com",
                    LastVisited = DateTime.Now.AddDays(2),
                    PhoneNumber = "08109502104",
                    TotalAMountSpent = 500000,
                    UserId = 4
                },
                new Customer
                {
                    Id = 2,
                    Name = "William 2",
                    Address = "kfc road 2",
                    CreatedOn = DateTime.Now,
                    Email = "mckensie2@gmail.com",
                    LastVisited = DateTime.Now.AddDays(2),
                    PhoneNumber = "08109502100",
                    TotalAMountSpent = 500000,
                    UserId = 2
                },
                new Customer
                {
                    Id = 3,
                    Name = "William 5",
                    Address = "kfc road 5",
                    CreatedOn = new DateTime(2020, 08, 10),
                    Email = "mckensie5@gmail.com",
                    LastVisited = DateTime.Now.AddDays(2),
                    PhoneNumber = "08109502105",
                    TotalAMountSpent = 500000,
                    UserId = 5
                });

            //User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "William 1",
                    Address = "kfc road 1",
                    CreatedOn = DateTime.Now,
                    Email = "mckensie1@gmail.com",
                    PhoneNumber = "08109502101",
                    UserType = UsersType.Affiliate.ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-50),
                    Gender = "male",
                    IsActive = true,
                },  
                new User
                {
                    Id = 2,
                    Name = "William 2",
                    Address = "kfc road 2",
                    CreatedOn = DateTime.Now,
                    Email = "mckensie2@gmail.com",
                    PhoneNumber = "08109502101",
                    UserType = UsersType.Customer.ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-40),
                    Gender = "male",
                    IsActive = true,
                },
                new User
                {
                    Id = 3,
                    Name = "William 3",
                    Address = "kfc road 3",
                    CreatedOn = new DateTime(2018, 08, 10),
                    Email = "mckensie3@gmail.com",
                    PhoneNumber = "08109502103",
                    UserType = UsersType.Employee.ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Gender = "male",
                    IsActive = true,
                },
                new User
                {
                    Id = 4,
                    Name = "William 4",
                    Address = "kfc road 4",
                    CreatedOn = new DateTime(2018, 10, 10),
                    Email = "mckensie4@gmail.com",
                    PhoneNumber = "08109502104",
                    UserType = UsersType.Customer.ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-34),
                    Gender = "female",
                    IsActive = true,
                },
                new User
                {
                    Id = 5,
                    Name = "William 5",
                    Address = "kfc road 4",
                    CreatedOn = new DateTime(2020, 08, 10),
                    Email = "mckensie5@gmail.com",
                    PhoneNumber = "08109502105",
                    UserType = UsersType.Customer.ToString(),
                    DateOfBirth = DateTime.Now.AddYears(-34),
                    Gender = "female",
                    IsActive = true,
                });

            //Discount 
            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    Name = UsersType.Affiliate.ToString(),
                    DiscountType = DiscountTypes.Percentage.ToString(),
                    Value = "10",
                    CreatedOn = DateTime.Now
                },
                new Discount
                {
                    Id = 2,
                    Name = UsersType.Employee.ToString(),
                    DiscountType = DiscountTypes.Percentage.ToString(),
                    Value = "30",
                    CreatedOn = DateTime.Now
                },
                new Discount
                {
                    Id = 3,
                    Name = UsersType.Customer.ToString(),
                    DiscountType = DiscountTypes.Flat.ToString(),
                    Value = "5",
                    CreatedOn = DateTime.Now
                },
                new Discount
                {
                    Id = 4,
                    Name = "Default",
                    DiscountType = DiscountTypes.Flat.ToString(),
                    Value = "5",
                    CreatedOn = DateTime.Now
                }
            );
        }
    }
}