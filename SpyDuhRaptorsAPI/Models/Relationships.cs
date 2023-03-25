namespace SpyDuhRaptorsAPI.Models
{
    public class Relationships
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RelatedUserId { get; set; }
        public bool IsFriend { get; set; }
    }
}
