using Core.Entities.Base;
using Core.Utilities.Attributes;

namespace Core.Entities
{
    public class Quiz:BaseEntity
    {
        public string QuizTitle { get; set; }
        public string QuizImage { get; set; }
        public string VariantA { get; set; }
        public string VariantB { get; set; }
        public string VariantC { get; set; }
        public string VariantD { get; set; }
        public string VariantE { get; set; }
        public string CorrectVariant { get; set; }
        public QuizCategory QuizCategory { get; set; }
        public int QuizCategoryId { get; set; }
        public string? QuizDescription { get; set; }
    }
}
