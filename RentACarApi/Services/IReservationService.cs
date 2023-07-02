using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationAsync(int reservationId);
        Task<List<ReservationView>> GetAllReservationsAsync();
        Task<ManagerResponse> CreateReservationAsync(ReservationViewModel model);
        Task<ManagerResponse> UpdateReservationAsync(int reservationId, ReservationViewModel model);
        Task<ManagerResponse> DeleteReservationAsync(int reservationId);
    }
}
