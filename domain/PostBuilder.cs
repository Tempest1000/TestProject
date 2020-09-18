namespace TestProject.domain
{
    public class PostBuilder
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }

        public PostBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public PostBuilder WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        public PostBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public PostBuilder WithBody(string body)
        {
            Body = body;
            return this;
        }

        public PostDto Build()
        {
            return new PostDto(this);
        }
    }
}
