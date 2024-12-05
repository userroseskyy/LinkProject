namespace LinkProject.Areas.Profile.Models.User
{
    
    public class UserProfile
    {
        public string Id { get; set; } 
        public string UserId { get; set; } 
        public string FullName { get; set; } 
        public string Bio { get; set; } 
        public string ProfilePictureUrl { get; set; } 
        public ICollection<UserLink> Links { get; set; } 
    }
}
