using Microsoft.AspNetCore.Mvc;
namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AzureBlobController : ControllerBase
{
    private readonly IAzureBlobService _blobService;
    private readonly ILogger<AzureBlobController> _logger;

    public AzureBlobController(IAzureBlobService blobService, ILogger<AzureBlobController> logger)
    {
        _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        _logger = logger ?? throw new ArgumentNullException();
    }
    [HttpGet(nameof(Get))]
    public async Task<IActionResult> Get()
    {        
        List<BlobDto>? files = await _blobService.ListAsync();
        return StatusCode(StatusCodes.Status200OK, files);
    }
    [HttpPost(nameof(Upload))]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        BlobResponseDto? response = await _blobService.UploadAsync(file);
        if (response.Error == true)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        }
        else
        {
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
    [HttpGet("{filename}")]
    public async Task<IActionResult> Download(string filename)
    {
        BlobDto? file = new BlobDto();
        file = await _blobService.DownloadAsync(filename);        
        if (file == null)
        {            
            return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
        }
        else
        {            
            return File(file.Content, file.ContentType, file.Name);
        }
    }
    [HttpDelete("filename")]
    public async Task<IActionResult> Delete(string filename)
    {
        BlobResponseDto response = await _blobService.DeleteAsync(filename);        
        if (response.Error == true)
        {            
            return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
        } else
        {            
            return StatusCode(StatusCodes.Status200OK, response.Status);
        }
    }
}