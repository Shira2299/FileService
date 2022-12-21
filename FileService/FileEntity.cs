namespace FileService
{
    public class FileEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string Url { get; set; }

        public string ContentType { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }   

    }
}
