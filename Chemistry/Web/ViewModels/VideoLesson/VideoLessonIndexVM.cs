namespace Web.ViewModels.VideoLesson
{
    public class VideoLessonIndexVM
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryTitle { get; set; }
        public string Title { get; set; }
        public List<string> videoLessonCategories { get; set; }
        public bool IsPaid { get; set; }

        //  public List<Core.Entities.VideoLesson> VideoLesson { get; set; }
    }
}
