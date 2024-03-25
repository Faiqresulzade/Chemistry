using Core.Entities.Base;

namespace Core.Entities
{
    public class VideoLessonCategory:BaseEntity
    {
        public string CategoryTitle { get; set; }
        public List<VideoLesson> VideoLessons { get; set; }
    }
}
