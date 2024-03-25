using Core.Entities.Base;

namespace Core.Entities
{
    public sealed class QuizAnswer : BaseEntity
    {
        public int WrongCount { get; set; }
        public int CorrectCount { get; set; }
        public string UserName { get; set; }
    }
}
