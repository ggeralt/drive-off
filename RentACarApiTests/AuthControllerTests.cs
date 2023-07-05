using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACarApi;
using RentACarApi.Controllers;
using RentACarApi.Model;
using RentACarApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarApiTests
{
    public class AuthControllerTests
    {
        //TestDatabase db;

        //AuthControllerTests()
        //{
        //    this.db = new TestDatabase();
        //}

        //[Fact]
        //public async void ConfirmShouldReturnOkObjectResult()
        //{
            //using (IServiceScope scope = db.serviceProvider.CreateScope())
            //{
                //arange
                //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                //{
                //    c.AddProfile(new AutoMapperProfile());
                //});
                //IMapper mapper = mapperConfiguration.CreateMapper();
                //db.FillDatabase(context);

                //AuthController authController = new AuthController(new UserService(userManager),, new Microsoft.AspNetCore.Http.HttpContextAccessor());

                //act
                //var result = await adminController.Confirm(1);


                //assert
               ///Assert.IsType<OkObjectResult>(result);
            //}
        //}
    }
}
