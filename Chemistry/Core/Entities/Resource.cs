using Core.Entities.Base;

namespace Core.Entities
{
    public class Resource : BaseEntity
    {
        public string  Title { get; set; }
        public string Image { get; set; }

        public string? Pdf {  get; set; }

        public string? Link { get; set; }
    }
}