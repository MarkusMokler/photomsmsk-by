using System;

namespace FileService.Classes.Files
{
    public interface IStorage
    {
        FileInformation SaveFile(FileData fileData);
        ImageFileInformation SaveImages(FileData fileData);
        string GetUrl(Guid id);
        byte[] GetContent(FileInformation information);
    }
}