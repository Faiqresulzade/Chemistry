using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class VideoLessonRepository:Repository<VideoLesson>,IVideoLessonRepository
    {
        public VideoLessonRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
