namespace RentACarApi.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool HasCertificate { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<Picture> Pictures { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Reservation> Reservations { get; set; }
        public string Location { get; set; }
    }
}
