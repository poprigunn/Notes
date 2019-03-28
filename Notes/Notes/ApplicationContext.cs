using Microsoft.EntityFrameworkCore;

namespace Notes
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public DbSet<Note> Notes { get; set; }

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
