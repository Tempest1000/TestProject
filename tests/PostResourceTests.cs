using System;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RA;
using TestProject.domain;
using TestProject.helpers;

namespace TestProject.tests
{
    /// <summary>
    /// These tests are targeted for the Post resource and are specific to post functionality
    /// </summary>
    [TestFixture]
    [AllureNUnit]
    public class PostResourceTests : TestBase
    {
        private const string Name = "Post Resource Tests";

        /// <summary>
        /// Note: This assumes the historical data set is static 
        /// and will not be archived, which would change post resource GET results
        /// </summary>
        /// <param name="id">The Id of the Post Resource</param>
        /// <param name="title">The expected Title of this Post Resource</param>
        [Test, TestCaseSource(typeof(TestDataProviders), "PostResourceTestCases")]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Get Single Post By Id")]
        public void GetSinglePostById(int id, string title)
        {
            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Header("CharSet", "utf-8")
              .When()
                .Get($"https://jsonplaceholder.typicode.com/posts/{id}")
              .Then();

            var test = responseContext.Retrieve(x => x.ToString());
            Console.WriteLine(test);

            responseContext.TestBody(
                    "Verify an existing single post is returned with an HTTP GET",
                x =>
                {
                    int.TryParse(x.id.ToString(), out int actualId);
                    Assert.AreEqual(id, actualId);
                    Assert.AreEqual(title, x.title.ToString());
                    return x.id == id && x.title == title;
                })
                .Assert("Verified");
        }

        [Test]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Get All Posts By User")]
        public void GetAllPostsByUserWithId()
        {
            int expectedPostCount = 10;

            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Header("CharSet", "utf-8")
              .When()
                .Get("https://jsonplaceholder.typicode.com/posts?userId=1")
              .Then();

            responseContext.TestBody("Verify all posts for a user are returned by Id with an HTTP GET",
              x =>
              {
                  Assert.AreEqual(expectedPostCount, x.Count);
                  return x.Count == expectedPostCount;
              })
              .Assert("Verified");
        }

        [Test]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Get Nested Comments for a Post")]
        public void GetAllNestedCommentsForASpecificPost()
        {
            int expectedCommentCount = 5;

            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Header("CharSet", "utf-8")
              .When()
                .Get("https://jsonplaceholder.typicode.com/posts/1/comments")
              .Then();

            responseContext.TestBody(
              "Verify all comments are found for a specific post found with an HTTP GET",
              x =>
              {
                  Assert.AreEqual(expectedCommentCount, x.Count);
                  return x.Count == expectedCommentCount;
              })
              .Assert("Verified");
        }

        [Test]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Create A New Post")]
        public void CreateANewPost()
        {
            var postDto = new PostBuilder().Build();
            string payload = JsonConvert.SerializeObject(postDto);

            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Body(payload)
              .When()
                .Post("https://jsonplaceholder.typicode.com/posts")
              .Then();

            responseContext.TestBody(
                    "Verify a new Post resource can be created with HTTP POST by confirming the Id returned in the response",
                    x =>
                    {
                        int.TryParse(x.id.ToString(), out int actualId);
                        Assert.AreEqual(101, actualId);
                        return x.id == 101;
                    });

            responseContext.TestStatus(
                    "Verify a new Post resource can be created with HTTP POST by confirming the response HTTP status code",
                    x =>
                    {
                        Assert.AreEqual(201, x);
                        return x == 201;
                    });


            responseContext.AssertAll();
        }

        [Test]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Update an Existing Post")]
        public void UpdateAnExistingPost()
        {
            int expectedId = 1;

            var postDto = new PostBuilder()
                .WithTitle("Put")
                .Build();

            string payload = JsonConvert.SerializeObject(postDto);

            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Body(payload)
              .When()
                .Put($"https://jsonplaceholder.typicode.com/posts/{expectedId}")
              .Then();

            responseContext.TestBody(
                    "Verify an existing Post resource can be updated with HTTP PUT",
                    x =>
                    {
                        int.TryParse(x.id.ToString(), out int actualId);
                        Assert.AreEqual(expectedId, actualId);
                        return x.id == expectedId;
                    });
        }

        [Test]
        [AllureTag("Post Resource Tests")]
        [AllureStory("Delete an Existing Post")]
        public void DeleteAnExistingPost()
        {
            var responseContext = new RestAssured()
                .Given()
                  .Name(Name)
                  .Header("Content-Type", "application/json")
                .When()
                  .Delete("https://jsonplaceholder.typicode.com/posts/1")
                .Then();

            responseContext.TestStatus(
                    "Verify an existing Post resource can be deleted with HTTP DELETE by confirming the response HTTP status code",
                    x =>
                    {
                        Assert.AreEqual(200, x);
                        return x == 200;
                    })
                .Assert("Verify");
        }
    }
}
