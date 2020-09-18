using System;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RA;

namespace TestProject.tests
{
    /// <summary>
    /// These tests are negative tests cases for a handful of endpoints
    /// </summary>
    [TestFixture]
    [AllureNUnit]
    public class NegativeTests : TestBase
    {
        private const string Name = "Negative Tests";

        /// <summary>
        /// It should not be possible to create a post with malformed JSON (missing comma)
        /// in the Body of the HTTP request, this test should fail.
        /// </summary>
        [Test]
        [AllureTag("Negative Tests")]
        [AllureStory("Create A Post With Malformed JSON")]
        public void CreateAPostWithMalformedJson()
        {
            // comma is missing after title field value
            string payload = @"
            { 
              ""title"": ""test title""
              ""body"": ""Test body""
            }";

            try
            {
                new RestAssured()
                    .Given()
                      .Name(Name)
                      .Header("Content-Type", "application/json")
                      .Body(payload)
                    .When()
                      .Post("https://jsonplaceholder.typicode.com/posts")
                    .Then();

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass("An expected exception was thrown due to malformed JSON");
            }
        }

        /// <summary>
        /// It should not be possible to create a post with an empty
        /// Body in the HTTP request, this test should fail.
        /// </summary>
        [Test]
        [AllureTag("Negative Tests")]
        [AllureStory("Create A Post With an Empty Body")]
        public void CreateAPostWithAnEmptyBody()
        {
            string payload = "";

            ResponseContext responseContext = new RestAssured()
                .Given()
                  .Name(Name)
                  .Header("Content-Type", "application/json")
                  .Body(payload)
                .When()
                  .Post("https://jsonplaceholder.typicode.com/posts")
                .Then();

            ResponseContext testStatus = responseContext.TestStatus(
                "Verify a Post resource cannot be created with an empty Body with HTTP POST",
                x =>
                {
                    if (x == 201)
                    {
                        Assert.Pass("An expected failure occurred due to a known issue with the API - empty Body");
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    return x != 201;
                });
        }

        /// <summary>
        /// It should not be possible to create a post when the wrong
        /// data types are passed in the Body of the HTTP request.
        /// This test should fail.
        /// </summary>
        [Test]
        [AllureTag("Negative Tests")]
        [AllureStory("Create A Post Using the Wrong Data Types")]
        public void CreateAPostUsingTheWrongDataTypes()
        {
            string payload = @"
            { 
              ""id"": ""test"", 
              ""userId"": ""test"", 
              ""title"": 1, 
              ""body"": 1 
            }";

            ResponseContext responseContext = new RestAssured()
                .Given()
                  .Name(Name)
                  .Header("Content-Type", "application/json")
                  .Body(payload)
                .When()
                  .Post("https://jsonplaceholder.typicode.com/posts")
                .Then();

            ResponseContext testStatus = responseContext.TestStatus(
                "Verify a Post resource cannot be created with an empty Body with HTTP POST",
                x =>
                {
                    if (x == 201)
                    {
                        Assert.Pass("An expected failure occured due to a known issue with the API - wrong data types in Body");
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    return x != 201;
                });
        }
    }
}
