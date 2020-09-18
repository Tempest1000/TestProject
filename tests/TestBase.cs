using NUnit.Framework;
using TestProject.helpers;

namespace TestProject.tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            TestHelper.InitializeSystem();
        }
    }
}
