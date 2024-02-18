using BambooCardAPI.Models;

namespace BambooCardAPI.Services.Interface
{
    public interface IStoryService
    {
         public Task<IEnumerable<Story>?> GetBestStories(int recordCount);
    }
}
