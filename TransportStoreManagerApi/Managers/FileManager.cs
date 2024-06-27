using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Repositories;

namespace TransportStoreManagerApi.Managers;

public class FileManager: IFileManager
{
    private readonly IBaseRepository<BlobFile> _fileRepository;

    public FileManager(IBaseRepository<BlobFile> fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<long> SaveFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var newFile = new BlobFile();
        newFile.Data = memoryStream.ToArray();
        await _fileRepository.AddAsync(newFile);

        return newFile.Id;
    }
    
}