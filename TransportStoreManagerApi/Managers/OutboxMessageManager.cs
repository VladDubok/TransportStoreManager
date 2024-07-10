using System.Text.Json;
using TransportStoreManagerApi.Data.Entities;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Messages;
using TransportStoreManagerApi.Models.Dtos;

namespace TransportStoreManagerApi.Managers;

public class OutboxMessageManager : IOutboxMessageManager
{
    private readonly Dictionary<string, Func<OutboxMessage, Task>> _strategies = new();
    private readonly IFileManager _fileManager;

    public OutboxMessageManager(IFileManager fileManager)
    {
        _fileManager = fileManager;
        _strategies.Add("product-file-uploaded", ProcessProductFileUploadMessageHandler);
    }
    
    public Func<OutboxMessage, Task> GetStrategy(string messageName)
    {
        // TODO: If strategy not exist, throw NotFoundMessageHandlerException
        return _strategies[messageName];
    }
    
     private async Task ProcessProductFileUploadMessageHandler(OutboxMessage message)
    {
        var productFileUploadedMessage = JsonSerializer.Deserialize<ProductFileUploadedMessage>(message.Data);
        var products = await _fileManager.GetDataFromCsv<FileProductDto>(productFileUploadedMessage.FileId);
        
        // update & insert products
        
    }
}