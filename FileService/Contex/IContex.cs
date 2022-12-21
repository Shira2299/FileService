using Microsoft.EntityFrameworkCore;

namespace FileService.Contex
{
    public interface IContex
    {
        DbSet<FileEntity> Files { get; set; }
    }
}
