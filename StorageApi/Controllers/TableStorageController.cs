using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TableStorageController : ControllerBase
{
    private readonly ITableStorageService _storageService;

    public TableStorageController(ITableStorageService storageService)
    {
        _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
    }

    [HttpGet("GetEntityAsync")]
    [ActionName(nameof(GetAsync))]
    public async Task<IActionResult> GetAsync([FromQuery] string partitionKey, string rowKey)
    {
        return Ok(await _storageService.GetEntityAsync(partitionKey, rowKey));
    }

    [HttpPost("InsertEntityAsync")]
    public async Task<IActionResult> PostAsync([FromBody] ProductEntity entity)
    {
        entity.PartitionKey = entity.Category;

        string Id = Guid.NewGuid().ToString();
        entity.Id = Id;
        entity.RowKey = Id;

        var createdEntity = await _storageService.UpsertEntityAsync(entity);
        return CreatedAtAction(nameof(GetAsync), createdEntity);
    }

    [HttpPut("UpdateEntityAsync")]
    public async Task<IActionResult> PutAsync([FromBody] ProductEntity entity)
    {
        entity.PartitionKey = entity.Category;
        entity.RowKey = entity.Id;

        return Ok(await _storageService.UpsertEntityAsync(entity));
        
    }

    [HttpDelete("DeleteEntityAsync")]
    public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
    {
        return Ok(await _storageService.DeleteEntityAsync(category, id));
    }
}