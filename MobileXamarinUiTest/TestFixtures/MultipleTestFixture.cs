/* Test Fixture for multiple Setup Fixture
 * Output:
 * RootFixtureSetup:OneTimeSetUp
 * FixtureSetup:OneTimeSetUp
 * Tests:MultipleTestFixture
 * Tests:OneTimeSetUp
 * Tests:SetUp
 * Tests:TC_00001_Test_Case_Title
 * Tests:TearDown
 * Tests:SetUp
 * Tests:TC_00002_Test_Case_Title
 * Tests:TearDown
 * Tests:OneTimeTearDown
 * FixtureSetup:OneTimeTearDown
 * RootFixtureSetup:OneTimeTearDown
 */

/* First Layer */
using NUnit.Framework;
using Xamarin.UITest;

[SetUpFixture]
public class RootFixtureSetup
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
    }
}

namespace Mobile.Xamarin.UiTest.TestFixtures
{
    /* Second Layer */
    [SetUpFixture]
    public class FixtureSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }
    }

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public abstract class MultipleTestFixture
    {
        protected IApp _app => AppManager.App;

        /* Platform Constructor */
        public MultipleTestFixture(Platform platform)
        {
            AppManager.Platform = platform;
        }

        /* Single App Launch */
        [OneTimeSetUp]
        public void AppLaunch()
        {
            AppManager.StartApp();
        }

        /* Setup Before Executing the test */
        [SetUp]
        public void BeforeEachTest()
        {
        }

        /* Setup after Executing the test */
        [TearDown]
        public void AfterEachTest()
        {
        }

        [OneTimeTearDown]
        public void Cleanup()
        {

        }
    }
}
