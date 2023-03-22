namespace SpyDuhRaptorsAPI.Models
{
    public record class UserDto(
        int Id, 
        string Name, 
        string UserName, 
        string HandlerName, 
        string Email, 
        string CountryName, 
        string AgencyName, 
        string CurrentAssignmentName);
}
