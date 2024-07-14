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
    private readonly IProductManager _productManager;

    public OutboxMessageManager(
        IFileManager fileManager,
        IProductManager productManager)
    {
        _fileManager = fileManager;
        _productManager = productManager;
        _strategies.Add("product-file-uploaded", ProcessProductFileUploadMessageHandler);
    }
    
    public Func<OutboxMessage, Task> GetMessageHandler(string messageName)
    {
        // TODO: If handler not exist, throw NotFoundMessageHandlerException
        return _strategies[messageName];
    }
    
     private async Task ProcessProductFileUploadMessageHandler(OutboxMessage message)
    {
        var productFileUploadedMessage = JsonSerializer.Deserialize<ProductFileUploadedMessage>(message.Data);
        var products = await _fileManager.GetDataFromCsv<UploadFileProductDto>(productFileUploadedMessage.FileId);
        
        foreach (var fileProductDto in products)
        {
            fileProductDto.CustomerId = productFileUploadedMessage.CustomerId;
        }
        
        await _productManager.UpdateFromFile(products);
    }
}