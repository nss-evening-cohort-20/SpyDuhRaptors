namespace SpyDuhRaptorsAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string Agency { get; set; }
        public string Type { get; set; }
        public string Assignment { get; set; }
        public string AssignmentLocation { get; set; }
        public DateTime StartTS { get; set; }
        public DateTime EndTS { get; set; }
        public bool IsUnderCover { get; set; }
    }
}
