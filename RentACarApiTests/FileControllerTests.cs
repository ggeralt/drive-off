using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class FileControllerTests
    {
        TestDatabase db;

        public FileControllerTests()
        {
            db = new TestDatabase();
        }

        [Fact]
        public async void DeleteShouldReturnOk()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                FileController fileController = new FileController(new FileService(context));


                //act
                var result = await fileController.Delete(1);


                //assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void DeleteShouldReturnNotFound()
        {
            using (IServiceScope scope = db.serviceProvider.CreateScope())
            {
                //arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mapperConfiguration.CreateMapper();
                db.FillDatabase(context);

                FileController fileController = new FileController(new FileService(context));


                //act
                var result = await fileController.Delete(12);


                //assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
