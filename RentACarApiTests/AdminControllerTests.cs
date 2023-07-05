using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentACarApi;
using RentACarApi.Controllers;
using RentACarApi.Model;
using RentACarApi.Services;
using RentACarShared;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarApiTests
{
    public class AdminControllerTests
    {
        TestDatabase db;

        public AdminControllerTests()
        {
            db = new TestDatabase();
        }

        [Fact]
        public async void ConfirmShouldReturnOkObjectResult()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.Confirm(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void ConfirmShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.Confirm(99);


                //assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async void GetAllNonConfirmedVehiclesShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.GetAllNonConfirmedVehicles();


                //assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public async void GetNonConfirmedVehicleShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.GetNonConfirmedVehicle(1);


                //assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public async void GetAllUsersShouldRetrunOK()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.GetAllUsers();


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteAccountShouldReturnOk()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.DeleteAccount("4");


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteAccountShouldReturnBadRequest()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.DeleteAccount("999");


                //assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteAccountShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.DeleteAccount(null);


                //assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async void DeleteVehicleShouldReturnOk()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.DeleteVehicle(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteVehicleShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                AdminController adminController = new AdminController(new AdminService(context, userManager, mapper));


                //act
                var result = await adminController.DeleteVehicle(99999);


                //assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

    }
}

