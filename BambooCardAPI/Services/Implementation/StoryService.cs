using BambooCardAPI.Models;
using BambooCardAPI.Services.Interface;
using BambooCardAPI.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BambooCardAPI.Services.Implementation
{
    public class StoryService : IStoryService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly int _cacheTTL;
        private readonly string _bestStoryKey;

        public StoryService(IMemoryCache memoryCache, IConfiguration configuration )
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
            _cacheTTL = _configuration.GetValue<int>("Config:CacheTTLInMin");
            _bestStoryKey = _configuration.GetValue<string>("Config:BestStoriesKey");
        }

        public async Task<IEnumerable<Story>?> GetBestStories(int recordCount)
        {
            IEnumerable<Story>? stories = new List<Story>();
            if (!_memoryCache.TryGetValue(_bestStoryKey, out stories))
            {
                await UpdateBestStoriesToDB();
                _memoryCache.TryGetValue(_bestStoryKey, out stories);
            }
            else
            {
                var lib = new HackerNewsLib();
                var ids = await lib.GetBestStoryIDs();
                if (stories != null && stories.Count() != ids.Count())
                {
                   await UpdateBestStoriesToDB();
                    _memoryCache.TryGetValue(_bestStoryKey, out stories);
                }
            }

            return stories?.Take(recordCount);

        }

        private async Task UpdateBestStoriesToDB()
        {

            var lib = new HackerNewsLib();
            var storeis = await lib.GetAllBestStories();
            if(storeis!=null)
            {
                _memoryCache.Remove(_bestStoryKey);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheTTL)); // Cache for 10 minutes

                _memoryCache.Set(_bestStoryKey, storeis.OrderByDescending(x=>x.score), cacheEntryOptions);

            }
        }
    }
}
