using Core.Entities.Base;

namespace Core.Entities
{
    public class VideoLesson:BaseEntity
    {
        public bool İsPaid { get; set; } 
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Video { get; set; }
        public VideoLessonCategory Category { get; set; }
        public int CategoryID { get; set; }
    }
}
