using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class QuizRepository:Repository<Quiz>,IQuizzesRepository
    {
        public QuizRepository(AppDbContext appDbContext):base(appDbContext){}

    }
}
