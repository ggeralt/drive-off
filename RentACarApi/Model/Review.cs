namespace RentACarApi.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
