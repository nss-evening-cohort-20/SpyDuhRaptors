using System.Diagnostics.Metrics;

namespace SpyDuhRaptorsAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int HandlerId { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }
        public int AgencyId { get; set; }
        public int AssignmentId { get; set; }
    }
}

