namespace FileService.Classes.Files
{
    public class FileData
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public long Size { get { return (Data != null ? Data.LongLength : 0L); } }
    }
}