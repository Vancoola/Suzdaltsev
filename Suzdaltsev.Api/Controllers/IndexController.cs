using Microsoft.AspNetCore.Mvc;
using Suzdaltsev.Domain;
using Suzdaltsev.Domain.Interfaces;

namespace Suzdaltsev.Api.Controllers;

[ApiController]
[Route("[action]")]
public class IndexController(INodeRepo nodeRepo, ILogger<IndexController> logger) : ControllerBase
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile? file)
    {

        try
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string content = await reader.ReadToEndAsync();
                string[] lines = content.Split('\n');
                foreach (var line in lines)
                {
                    var splitted = line.Split(':');
                    var segments = splitted[1].Split(',');
                    nodeRepo.Create(segments, splitted[0]);
                }
            }

            return Created();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured creating the node");
            return BadRequest();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get(string location)
    {
        
        var result = nodeRepo.FindAdvertisers(location.Split('/', StringSplitOptions.RemoveEmptyEntries));
        return Ok(result);
    }
    
}