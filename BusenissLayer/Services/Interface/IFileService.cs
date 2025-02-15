namespace BusinessLayer.Services.Interface;

public interface IFileService
{
    public Task<string> UploadAsync(IFormFile file, string _environment);
    public bool DeletePhysicalFile(string path);
}