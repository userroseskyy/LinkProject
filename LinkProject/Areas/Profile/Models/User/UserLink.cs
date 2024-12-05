namespace LinkProject.Areas.Profile.Models.User
{
   
    public class UserLink
    {
        public string Id { get; set; } 
        public string UserProfileId { get; set; } 
        public UserProfile UserProfile { get; set; } 
        public string Title { get; set; } 
        public string Url { get; set; } 
        public string IconUrl { get; set; } 
    }
}
