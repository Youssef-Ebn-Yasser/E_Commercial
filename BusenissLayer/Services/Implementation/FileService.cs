namespace BusinessLayer.Services.Implementation;

public class FileService : IFileService
{
    #region Fields
    private readonly string location = "/Added/images/";
    #endregion

    #region Constructor
    #endregion

    #region Methods
    public async Task<string> UploadAsync(Microsoft.AspNetCore.Http.IFormFile file, string _environment)
    {
        if (!(file.Length > 0))
            return "Failed";

        var path = _environment + location;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        #region  make a new path name
        var extension = Path.GetExtension(file.FileName);
        var filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
        #endregion

        using (FileStream stream = File.Create(path + filename))
        {
            await file.CopyToAsync(stream);
            stream.Flush();

            return $"{location}{filename}";
        }
    }

    public bool DeletePhysicalFile(string path)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + path);  // return full path of image
        File.Delete(directoryPath);

        return true;
    }
    #endregion
}