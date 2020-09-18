using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RA;
using TestProject.helpers;

namespace TestProject.tests
{
    /// <summary>
    /// These are smoke tests that give a quick sense for the overall health of the API
    /// by using just HTTP GET calls
    /// </summary>
    [TestFixture]
    [AllureNUnit]
    public class SmokeTests : TestBase
    {
        private const string Name = "Smoke Tests";

        /// <summary>
        /// This test case uses a test data provider based on the 
        /// Resources specification section found here: https://jsonplaceholder.typicode.com/
        /// Note: This assumes this data set is static 
        /// and will not be archived, which would change resource record counts
        /// </summary>
        /// <param name="resource">The Resource being requested</param>
        /// <param name="resourceCount">The expected count of Resource records returned</param>
        [Test, TestCaseSource(typeof(TestDataProviders), "ResourceCountTestCases")]
        [AllureTag("Smoke Tests")]
        [AllureStory("Get All Resources By Resource Name")]
        public void GetAll(string resource, int resourceCount)
        {
            ResponseContext responseContext = new RestAssured()
              .Given()
                .Name(Name)
                .Header("Content-Type", "application/json")
                .Header("CharSet", "utf-8")
              .When()
                .Get($"https://jsonplaceholder.typicode.com/{resource}")
              .Then();

            responseContext.TestBody(
                    $"Verify {resource} are returned when the {resource} endpoint is called with HTTP GET and no parameters",
                x =>
                {
                    Assert.AreEqual(resourceCount, x.Count);
                    return x.count() == resourceCount;
                })
                .Assert("Verified");
        }
    }
}