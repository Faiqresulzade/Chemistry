using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;

namespace DataAcces.Repositories.Concrete
{
    public class QuizAnswersRepository : Repository<QuizAnswer>,IQuizAnswersRepository
    {
        public QuizAnswersRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
