// Test Fixture for single Setup Fixture 
// Output:
// Tests:BaseTestFixture
// Tests:SetUp
// Tests:TC_00001_Test_Case_Title
// Tests:TearDown
// Tests:BaseTestFixture
// Tests:SetUp
// Tests:TC_00002_Test_Case_Title
// Tests:TearDown

using NUnit.Framework;
using Xamarin.UITest;

namespace Mobile.Xamarin.UiTest.TestFixtures
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public abstract class BaseTestFixture
    {
        protected IApp _app => AppManager.App;

        public BaseTestFixture(Platform platform)
        {
            AppManager.Platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            AppManager.StartApp();
        }

        [TearDown]
        public void Cleanup()
        {

        }
    }
}
