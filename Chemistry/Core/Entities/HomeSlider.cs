using Core.Entities.Base;

namespace Core.Entities
{
    public class HomeSlider:BaseEntity
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackRoundImage { get; set; }
    }
}
