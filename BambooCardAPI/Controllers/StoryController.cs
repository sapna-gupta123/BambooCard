using BambooCardAPI.Models;
using BambooCardAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BambooCardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        
        private readonly ILogger<StoryController> _logger;

        private readonly IStoryService _storyService;

        public StoryController(ILogger<StoryController> logger, IStoryService storyService)
        {
            _logger = logger;
            _storyService = storyService;
        }


        [HttpGet]
        public async Task<IEnumerable<Story>?> GetAllBestSotries(int recordCount)
        {
            return await _storyService.GetBestStories(recordCount);
        }
    }
}