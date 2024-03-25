using Core.Entities.Base;

namespace Core.Entities
{
    public class QuizCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public bool İsPaid { get; set; }
    }
}
