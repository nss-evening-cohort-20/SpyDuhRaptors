namespace SpyDuhRaptorsAPI.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartTS { get; set; }
        public DateTime EndTS { get; set; }
        public bool IsUnderCover { get; set; }
    }
}
