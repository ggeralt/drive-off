using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public class ReservationService : IReservationService
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ReservationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ManagerResponse> CreateReservationAsync(string userId, int vehicleId, ReservationViewModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find userId",
                    IsSuccess = false
                };
            }

            var vehicle = await context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find vehicleId",
                    IsSuccess = false
                };
            }
            
            if (model == null)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle model is null",
                    IsSuccess = false
                };
            }

            Reservation reservationModel = mapper.Map<Reservation>(model);
            reservationModel.applicationUser = user;
            reservationModel.Vehicle = vehicle;

            context.Reservations.Add(reservationModel);

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "New reservation Created",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to create new reservation",
                    IsSuccess = false
                };
            }
        }

        public async Task<ManagerResponse> DeleteReservationAsync(int reservationId)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);

            if (reservation == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to fiend reservationId",
                    IsSuccess = false
                };
            }
            context.Reservations.Remove(reservation);
            
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Reservation deleted",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to delete reservation",
                    IsSuccess = false
                };
            }
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var reservations = await context.Reservations.ToListAsync();
            return reservations;
        }

        public async Task<Reservation> GetReservationAsync(int reservationId)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);
            return reservation;
        }

        public async Task<ManagerResponse> UpdateReservationAsync(int reservationId, ReservationViewModel model)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find reservationId",
                    IsSuccess = false
                };
            }
            else if (model == null)
            {
                return new ManagerResponse
                {
                    Message = "Reservation model is null",
                    IsSuccess = false
                };
            }

            reservation = mapper.Map<Reservation>(model);

            context.Reservations.Update(reservation);

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Reservation updated",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to update reservation",
                    IsSuccess = false
                };
            }
        }
    }
}
