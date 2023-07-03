using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACarApi.Model;
using RentACarShared;
using System.Security.Claims;

namespace RentACarApi.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public ReservationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ManagerResponse> CreateReservationAsync(ReservationViewModel model)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                string userId = GetUserId();

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new ManagerResponse
                    {
                        Message = "Failed to find userId",
                        IsSuccess = false
                    };
                }

                var vehicle = await context.Vehicles.FindAsync(model.VehicleId);
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
                var reservations = await context.Reservations.Where(d => d.DateFrom <= model.DateTo && model.DateFrom <= d.DateTo && d.Vehicle.Id == model.VehicleId).ToListAsync();

                if (reservations.Count > 0)
                {
                    return new ManagerResponse
                    {
                        Message = "That date is already booked",
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
            }

            return new ManagerResponse
            {
                Message = "Failed to create new reservation",
                IsSuccess = false
            };
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

        public async Task<List<ReservationView>> GetAllReservationsAsync()
        {
            string userId = GetUserId();
            var reservations = await context.Reservations.Include(r => r.Vehicle).Where(r => r.applicationUser.Id == userId).ToListAsync();

            if (reservations != null)
            {
                List<ReservationView> reservationViewModels = new List<ReservationView>();

                foreach (var reservation in reservations)
                {
                    ReservationView reservationViewModel = new ReservationView
                    {
                        Id = reservation.Id,
                        DateFrom = reservation.DateFrom,
                        DateTo = reservation.DateTo,
                        Vehicle = reservation.Vehicle.Title
                    };
                    reservationViewModels.Add(reservationViewModel);
                }
                return reservationViewModels;
            }

            return null;
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
        private string GetUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
