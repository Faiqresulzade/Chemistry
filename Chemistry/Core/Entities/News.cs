using Core.Entities.Base;
namespace Core.Entities
{
    public class News :BaseEntity
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }
    }
}
