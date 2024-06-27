namespace TransportStoreManagerApi.Managers;

public interface IFileManager
{
    Task<long> SaveFile(IFormFile file);
}