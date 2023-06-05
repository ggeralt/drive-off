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
            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<PictureViewModel, Picture>();
            CreateMap<Picture, PictureViewModel>();
            CreateMap<ReservationViewModel, Reservation>();
        }
    }
}
