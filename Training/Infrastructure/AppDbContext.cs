using System.Data.Entity;
using Training.Models;
namespace Training.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }
            
        public DbSet<Student> Students { get; set; }
        public DbSet<Models.Training> Trainings { get; set; }
    }
}