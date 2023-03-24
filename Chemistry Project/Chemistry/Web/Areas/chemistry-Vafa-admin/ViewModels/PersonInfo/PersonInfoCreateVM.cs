namespace Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo
{
    public class PersonInfoCreateVM
    {
        public string FullName { get; set; }
        public string Info { get; set; }
        public string? PhotoPath{ get; set; }
        public IFormFile Photo { get; set; }
    }
}
