using Core.Entities;

namespace Web.ViewModels.Home
{
    /// <summary>
    /// VM for all models in Home
    /// </summary>
    public class HomeIndexVM
    {
        public PersonInfo GetPersonInfo { get; set; }
        public List<Students> Students { get; set; } = new List<Students>();
        public List<Core.Entities.News> News { get; set; } = new List<Core.Entities.News>();
        public List<HomeSlider> HomeSliders { get; set; } = new List<HomeSlider>();

        public List<Resource> Resources { get; set; } = new List<Resource>();
        public HomeMessageVM messageVM { get; set; }
        public Curseİnfo Curseİnfo { get; set; }

        public bool? MesageSended { get; set; }
    }
}
