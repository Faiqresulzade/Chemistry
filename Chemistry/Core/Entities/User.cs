using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
