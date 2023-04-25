using Microsoft.AspNetCore.Identity;

namespace RentACarApi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public bool HasDrivingLicence { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
