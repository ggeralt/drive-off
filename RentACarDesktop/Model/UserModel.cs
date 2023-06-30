namespace RentACarDesktop.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool HasDrivingLicence { get; set; }
    }
}
