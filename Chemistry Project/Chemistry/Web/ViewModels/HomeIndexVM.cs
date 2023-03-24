using Core.Entities;

namespace Web.ViewModels
{
    public class HomeIndexVM
    {
        public PersonInfo  GetPersonInfo { get; set; }
        public List<Students> Students { get; set; }
    }
}
