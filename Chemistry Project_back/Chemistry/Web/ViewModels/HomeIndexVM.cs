using Core.Entities;

namespace Web.ViewModels
{
    public class HomeIndexVM
    {
        public PersonInfo  GetPersonInfo { get; set; }
        public List<Students> Students { get; set; }
        public List<News> News { get; set; }
        public List<HomeSlider> HomeSliders { get; set; }
        public HomeLoginVM LoginVM { get; set; }
        public HomeRegisterVM RegisterVM { get; set; }
    }
}
