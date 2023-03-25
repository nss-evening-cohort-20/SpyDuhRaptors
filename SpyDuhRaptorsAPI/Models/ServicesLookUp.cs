namespace SpyDuhRaptorsAPI.Models
{
    public class ServicesLookUp:Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServicesLookUp Services{ get; set; }
    }
}
