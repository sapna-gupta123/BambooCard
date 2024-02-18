using BambooCardAPI.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BambooCardAPI.Utility
{
    public class HackerNewsLib
    {
        private readonly HttpClient _client;
        public HackerNewsLib() {
            _client = new HttpClient();
            LoadClient();
        }

        private void LoadClient()
        {
            _client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<int>> GetBestStoryIDs()
        {
            List<int> storyIDs = new List<int>();
                //GET Method
                HttpResponseMessage response = await _client.GetAsync("v0/beststories.json");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var ids = JsonConvert.DeserializeObject<List<int>>(res);
                    if(ids != null)
                    {
                        storyIDs = ids;
                    } 
                }
            

            return storyIDs;
        }

        public async Task<Story> GetBestStoryDetailById(string id)
        {
            Story? story = null;

                //GET Method
                HttpResponseMessage response = await _client.GetAsync($"v0/item/{id}.json");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    story = JsonConvert.DeserializeObject<Story>(res);
                }
            

            return story;
        }

        public async Task<IEnumerable<Story>> GetAllBestStories()
        {
            ConcurrentBag<Story> stories = new ConcurrentBag<Story>();
            
            var ids = await GetBestStoryIDs();

            Parallel.ForEach(ids, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, index =>
            {
                var story =  GetBestStoryDetailById(index.ToString());
                if (story != null)
                {
                    stories.Add(story.Result);

                }
                
            });
            return stories;
        }

    }
}
