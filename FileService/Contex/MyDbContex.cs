using Microsoft.EntityFrameworkCore;

namespace FileService.Contex
{
    public class MyDbContex : DbContext,IContex
    {
        public DbSet<FileEntity> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDBFileService;Trusted_Connection=True;");
        }
     
    }
}
