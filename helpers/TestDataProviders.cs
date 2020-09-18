namespace TestProject.helpers
{
    public class TestDataProviders
    {
        /// <summary>
        /// /posts 	    100 posts
        /// /comments 	500 comments
        /// /albums 	100 albums
        /// /photos 	5000 photos
        /// /todos 	    200 todos
        /// /users 	    10 users
        /// </summary>
        static object[] ResourceCountTestCases =
        {
            new object[] { "posts", 100 },
            new object[] { "comments", 500 },
            new object[] { "albums", 100 },
            new object[] { "photos", 5000 },
            new object[] { "todos", 200 },
            new object[] { "users", 10 }
        };

        static object[] PostResourceTestCases =
        {
            new object[] { 1, "sunt aut facere repellat provident occaecati excepturi optio reprehenderit" },
            new object[] { 2, "qui est esse" },
            new object[] { 3, "ea molestias quasi exercitationem repellat qui ipsa sit aut" },
            new object[] { 4, "eum et est occaecati" },
            new object[] { 5, "nesciunt quas odio" }
        };
    }
}
