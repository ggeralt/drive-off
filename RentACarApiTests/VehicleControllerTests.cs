using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RentACarApi;
using RentACarApi.Controllers;
using RentACarApi.Model;
using RentACarApi.Services;
using RentACarShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarApiTests
{
    public class VehicleControllerTests
    {
        TestDatabase db;

        public VehicleControllerTests()
        {
            db = new TestDatabase();
        }

        [Fact]
        public async void GetVehicleShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context,userManager,mapper,new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetVehicle(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetVehicleShouldReturnNotFound()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetVehicle(13);


                //assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public async void AddVehicleShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.AddVehicle(new RentACarShared.VehicleViewModel()
                {
                    Id = 2,
                    Title = "test",
                    Model = "test",
                    Brand = "test",
                    HasCertificate = true,
                    Type = "test",
                    Description = "test",
                    Location = "test",
                    Latitude = 1,
                    Longitude = 1,
                    Price = 12
                });


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }


        [Fact]
        public async void AddVehicleShouldReturnBadRequest()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.AddVehicle(new RentACarShared.VehicleViewModel()
                {
                    Id = 2,
                    Title = "test",
                    Model = "test",
                    Brand = "test",
                    HasCertificate = true,
                    Type = "test",
                    Description = "test",
                    Location = "test",
                    Latitude = 1,
                    Longitude = 1,
                    Price = 12
                });


                //assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async void UpdateVehicleShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.UpdateVehicle(new RentACarShared.VehicleViewModel()
                {
                    Id = 1,
                    Title = "testUpdated",
                    Model = "test",
                    Brand = "test",
                    HasCertificate = true,
                    Type = "test",
                    Description = "test",
                    Location = "test",
                    Latitude = 1,
                    Longitude = 1,
                    Price = 12,
                });


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetAllVehiclesShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetAllVehicles();


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetSearchedVehiclesShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetSearchedVehicles("Zagreb");


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetSearchedVehiclesShouldReturnNotFound()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetSearchedVehicles("");


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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.DeleteVehicle(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetReviewShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.GetReviews(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void CreateReviewShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.CreateReview(new ReviewViewModel()
                {
                    Id = 5,
                    User = "4",
                    VehicleId = 1
                });


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteReviewShouldReturnOk()
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

                VehicleController vehicleController = new VehicleController(new VehicleService(context, userManager, mapper, new HttpContextAccessor()));


                //act
                var result = await vehicleController.DeleteReview(33);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

    }
}
