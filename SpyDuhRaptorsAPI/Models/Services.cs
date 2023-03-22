namespace SpyDuhRaptorsAPI.Models
{
    public class Services
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public bool AvailibleToHire { get; set; }
        public Services Service { get; set; }

    }
}
