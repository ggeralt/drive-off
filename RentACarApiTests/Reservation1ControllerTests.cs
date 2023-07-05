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
    public class Reservation1ControllerTests
    {
        TestDatabase db;

        public Reservation1ControllerTests()
        {
            db = new TestDatabase();
        }

        [Fact]
        public async void GetAllReservationsShouldReturnOk()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.GetAllReservations();


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetReservationShouldReturnOk()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.GetReservation(2);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void GetReservationShouldReturnNotFound()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.GetReservation(123);


                //assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async void AddReservationShouldReturnOk()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.AddReservation(new ReservationViewModel()
                {
                    VehicleId = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.MaxValue
                });


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void AddReservationShouldReturnConflict()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.AddReservation(new ReservationViewModel()
                {
                    VehicleId = 2,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.MaxValue
                });


                //assert
                Assert.IsType<ConflictObjectResult>(result);
            }
        }

        [Fact]
        public async void UpdateReservationShouldReturnOk()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.UpdateReservation(2,new ReservationViewModel()
                {
                    VehicleId = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.MaxValue
                });


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void UpdateReservationShouldReturnBadRequest()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.UpdateReservation(234, new ReservationViewModel()
                {
                    VehicleId = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.MaxValue
                });


                //assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteReservationShouldReturnOk()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.DeleteReservation(2);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteReservationShouldReturnBadRequest()
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

                Reservation1Controller reservation1Controller = new Reservation1Controller(new ReservationService(context, userManager, new HttpContextAccessor(), mapper));


                //act
                var result = await reservation1Controller.DeleteReservation(233);


                //assert
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}

