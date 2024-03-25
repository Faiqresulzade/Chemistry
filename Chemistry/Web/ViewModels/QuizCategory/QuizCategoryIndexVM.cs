namespace Web.ViewModels.QuizCategory
{
    public class QuizCategoryIndexVM
    {
        public QuizCategoryIndexVM()
        {
            QuizCategories = new List<Core.Entities.QuizCategory>();
        }
        public List<Core.Entities.QuizCategory> QuizCategories { get; set; }
    }
}
