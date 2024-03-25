using Core.Entities.Base;

namespace Core.Entities
{
    public class Message:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
