using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels.Quiz
{
    public class QuizIndexVM
    {
        public QuizIndexVM()
        {
            Errors = new Dictionary<int, string>();
            Quizzes = new List<Core.Entities.Quiz>();
            //Description = new List<string>();
        }
        public Dictionary<int, string>? Errors { get; set; }
        public Dictionary<int, string>? Answers { get; set; }
        public List<Core.Entities.Quiz>? Quizzes { get; set; }

        //public List<string> Description { get; set; }

        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public bool IsFinished { get; set; }
    }
}
