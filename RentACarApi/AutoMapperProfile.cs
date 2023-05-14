using AutoMapper;
using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleViewModel, Vehicle>();
        }
    }
}
