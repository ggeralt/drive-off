using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentACarApi.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarApiTests
{
    public class TestDatabase
    {
        public ServiceProvider serviceProvider;

        public TestDatabase()
        {

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(c => c.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
            services.AddIdentity<ApplicationUser,IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddLogging();

            serviceProvider = services.BuildServiceProvider();
        }

        public void FillDatabase(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser
            {
                Id = "4",
                UserName = "testUser",
                NormalizedUserName = "TestUser",
                Email = "test@test.test",
                NormalizedEmail = "test@test.test",
                EmailConfirmed = true,
                PasswordHash = "123",
                SecurityStamp = "1234",
                ConcurrencyStamp = "12345",
                PhoneNumber = "321123",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                LockoutEnd = null,
                AccessFailedCount = 0,
                HasDrivingLicence = true,
            });

            context.Users.Add(new ApplicationUser
            {
                Id = "5",
                UserName = "testUserAdmin",
                NormalizedUserName = "TESTUSERADMIN",
                Email = "test2@test.test",
                NormalizedEmail = "test2@test.test",
                EmailConfirmed = true,
                PasswordHash = "321",
                SecurityStamp = "12345",
                ConcurrencyStamp = "123456",
                PhoneNumber = "3211237",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                LockoutEnd = null,
                AccessFailedCount = 0,
                HasDrivingLicence = true,
            });

            context.Vehicles.Add(new Vehicle
            {
                Id = 1,
                Title = "testCar",
                HasCertificate = true,
                Model = "F123",
                Brand = "Ferrari",
                Type = "Electric",
                Description = "Bla",
                ApplicationUser = context.Users.Find("4"),
                Location = "Zagreb",
                Latitude = 12,
                Longitude = 13,
                Price = 12.99,
            });

            context.Pictures.Add(new Picture
            {
                Id = 1,
                IsProfile = true,
                ImageData = new byte[5],
            });

            context.Reservations.Add(new Reservation
            {
                Id = 2,
                DateFrom = DateTime.Now,
                DateTo = DateTime.MaxValue,

            });

            context.Reviews.Add(new Review
            {
                Id = 33,
                Description = "blablabla",
            });

            context.Roles.Add(new IdentityRole
            {
                Id = "1",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "1111"
            });

            context.Roles.Add(new IdentityRole
            {
                Id = "2",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "222222222"
            });

            context.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "4",
            });

            context.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "5",
            });
        }
    }
}
