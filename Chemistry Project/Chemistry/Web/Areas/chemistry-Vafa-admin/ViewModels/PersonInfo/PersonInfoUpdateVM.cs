namespace Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo
{
    public class PersonInfoUpdateVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Info { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
