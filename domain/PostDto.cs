using Newtonsoft.Json;
using TestProject.helpers;

namespace TestProject.domain
{
    public class PostDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }

        public PostDto(PostBuilder builder)
        {
            this.Id = builder.Id == 0 ? Constants.Id : 0;
            this.UserId = builder.UserId == 0 ? Constants.UserId : 0;
            this.Title = builder.Title ?? Constants.Title;
            this.Body = builder.Body ?? Constants.Content;
        }
    }
}
