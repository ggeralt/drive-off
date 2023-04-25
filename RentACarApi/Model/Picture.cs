namespace RentACarApi.Model
{
    public class Picture
    {
        public int Id { get; set; }
        public bool IsProfile{ get; set; }
        public byte[] ImageData { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
