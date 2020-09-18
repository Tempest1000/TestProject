using System;
using System.IO;
using Allure.Commons;

namespace TestProject.helpers
{
    public class TestHelper
    {
        public static void InitializeSystem()
        {
            var env = Environment.GetEnvironmentVariable(AllureConstants.ALLURE_CONFIG_ENV_VARIABLE);

            if (string.IsNullOrEmpty(env))
            {
                Environment.SetEnvironmentVariable(
                    AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
                    Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));
            }

            var config = AllureLifecycle.Instance.JsonConfiguration;
        }
    }
}
