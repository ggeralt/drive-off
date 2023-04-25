namespace RentACarApi.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
