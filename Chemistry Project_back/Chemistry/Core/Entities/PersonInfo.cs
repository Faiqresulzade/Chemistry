using Core.Entities.Base;

namespace Core.Entities
{
    public class PersonInfo:BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
    }
}
