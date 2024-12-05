using System.ComponentModel.DataAnnotations.Schema;

namespace LinkProject.Areas.Profile.Models.User
{
    
    public class Icon
    {
        
        public string Id { get; set; } 
        public string Name { get; set; }
        public string Url { get; set; }
        
    }
}
