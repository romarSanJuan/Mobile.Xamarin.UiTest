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
 * 
 * Note:
 * A method decorated with a SetUp attribute will be executed before each test.
 * A method decorated with a TearDown attribute will be executed after each test.
 * A method decorated with a OneTimeSetUp attribute will be executed before any test is executed.
 */

/* First Layer Setup
 * add first layer setup before starting the test
 */
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
    /* Second Layer Setup
     * add second layer setup before starting the test
     */
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
