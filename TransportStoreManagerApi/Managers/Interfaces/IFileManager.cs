namespace TransportStoreManagerApi.Managers.Interfaces;

public interface IFileManager
{
    Task<long> SaveFile(IFormFile file);
    Task<IEnumerable<T>> GetDataFromCsv<T>(long fileId);
}