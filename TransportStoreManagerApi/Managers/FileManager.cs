using System.Globalization;
using System.Text;
using CsvHelper;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Repositories;
using TransportStoreManagerApi.Repositories.Interfaces;

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

    public async Task<IEnumerable<T>> GetDataFromCsv<T>(long fileId)
    {
        var blobFile = await _fileRepository.GetByIdAsync(fileId);
        using var memStream = new MemoryStream(blobFile.Data);
        using var reader = new StreamReader(memStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<T>();
    }
}