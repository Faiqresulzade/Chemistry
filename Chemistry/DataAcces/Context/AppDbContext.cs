using Core.Configurations;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Context
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<PersonInfo> PersonInfo { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VideoLesson> VideoLessons { get; set; }
        public DbSet<VideoLessonCategory> VideoLessonCategories { get; set; }
        public DbSet<Curseİnfo> Curseİnfos { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizCategory> QuizCategories { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        }

    }
}
