using Mobile.Xamarin.UiTest.TestFixtures;
using NUnit.Framework;
using Mobile.Xamarin.UiTest.Pages.MainPage;
using Mobile.Xamarin.UiTest.Pages.SubPage;
using Xamarin.UITest;

namespace Mobile.Xamarin.UiTest.AutomatedTests.RegressionTest
{
    public class Priority1TestCase : BaseTestFixture
    {
        public Priority1TestCase(Platform platform)
            : base(platform)
        {
        }

        /* Test Execution with Ordering */
        [Test, Order(1)]
        public void TC_0001_Test_Case_Title()
        {
            /* Accessing all actions and assertion available in each page */
            new MainPage()
                .MainPageAction01()
                .MainPageAssertion01();

            new SubPage()
                .SubPagePageAction01()
                .SubPageAssertAction01();
        }

        [Test, Order(2)]
        public void TC_0002_Test_Case_Title()
        {
            /* All actions and assertion can be reordered based on Test Case Steps */
            new MainPage()
                .MainPageAssertion01()
                .MainPageAction01();

            new SubPage()
                .SubPageAssertAction01()
                .SubPagePageAction01();
        }

        /* Skipping Test Execution */
        [Ignore("Reason"), Order(3)]
        public void TC_0003_Test_Case_Title()
        {
        }

    }
}
