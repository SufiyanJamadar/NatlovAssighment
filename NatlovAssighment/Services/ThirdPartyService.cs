using Newtonsoft.Json;

namespace NatlovAssighment.Services
{
    public class ThirdPartyService
    {
        private readonly HttpClient _httpClient;
        public ThirdPartyService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task<List<Post>> GetDataAsync()
        {
            var response = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            return JsonConvert.DeserializeObject<List<Post>>(response);
        }


        public class Post
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }

    }
}
