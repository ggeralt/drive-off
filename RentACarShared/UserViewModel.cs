namespace RentACarShared
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int? VehicleId { get; set; }
    }
}
