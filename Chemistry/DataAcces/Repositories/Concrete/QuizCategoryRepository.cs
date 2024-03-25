using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class QuizCategoryRepository:Repository<QuizCategory>,IQuizCategoryRepository
    {
        public QuizCategoryRepository(AppDbContext appDbContext):base(appDbContext){}
    }
}
