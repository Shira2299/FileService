using FileService;
using FileService.Contex;
using java.nio.file;
using java.security.cert;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        //DbSet<FileEntity> files = new DbSet<FileEntity>();
        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //var fileList = new List<FileEntity>();
        //fileList.Add(new FileEntity
        //{
        //    Id = 1,
        //    Name = "file name",
        //    Extension = "txt",
        //    CreateBy = 10,
        //    CreatedDate = DateTime.Now,
        //    LastUpdatedDate = DateTime.Now.AddDays(-1),
        //    ContentType = "text/plain",
        //    Url = "F:\Full Stack\FileService\Files\\dotnet.txt"   F:\Full Stack\FileService\Files\\file1.docx
        //});
        //app.MapGet("/files", () =>
        //{
        //    return fileList;
        //});
        IContex _contex = new MyDbContex();
        app.MapGet("/files", () =>
        {
            return _contex.Files.ToList();
        });
        app.MapGet("/files/id", (int id) =>
        {
            return _contex.Files.Find(id);
        });
        app.MapGet("/files/{id}/download", async (int id) =>
        {
            var file = _contex.Files.Find(id);

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.Url, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return Results.File(memory, file.ContentType, Path.GetFileName(file.Url));
        });
        //linq = where & find
        app.MapGet("/txt", () =>
        {
            return _contex.Files.Where(f => f.Extension == "txt");
        });
        app.MapGet("/id", () =>
        {
            return _contex.Files.Where(f => f.Id == 1);
        });
        app.MapGet("/gather", () =>   //לקבץ ולהחזיר במערך של אובייקטים
        {
            var file = _contex.Files
            .GroupBy(f => f.CreateBy)
            .Select(g => new { CreatedBy = g.Key, Files = g.Select(f => new { f.Id, f.Name, f.CreateBy }) });
         return file;
        });
        //לא הצלחתי את המיונים וגם להוריד קובץ מסוג word לא נתן מטבלה כנראה משהו לא מדויק הקובץ ה-5

        //app.MapPost("/files", async (IFormFile file) =>
        //{
        //    return file.Name;
        //});
        app.Run();
    }
}