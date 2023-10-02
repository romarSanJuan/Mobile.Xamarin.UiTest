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

        //  Test Execution with Ordering
        //  Test execution order can be defined,
        //  ordered tests are started in ascending order.
        //  By Default test execution is rrun alphabetically.

        [Test, Order(1)]
        public void TC_0001_Test_Case_Title()
        {
            //  Access all defined actions and assertions in each page
            new MainPage()
                .MainPageAction01()
                .MainPageAssertion01();

            new SubPage()
                .SubPagePageAction()
                .SubPageMovementAction();
        }

        [Test, Order(2)]
        public void TC_0002_Test_Case_Title()
        {
            //  All actions and assertions can be reordered
            // based on Test Case Steps

            new MainPage()
                .MainPageAssertion01()
                .MainPageAction01();

            new SubPage()
                .SubPageAssertAction()
                .SubPageGlobalFeatures();
        }

        //  Skipping Test Execution
        //  Add reason for ignoring the test

        [Ignore("Reason"), Order(3)]
        public void TC_0003_Test_Case_Title()
        {
            new MainPage()
               .MainPageAction02()
               .MainPageAction01();

            new SubPage()
                .SubPageAssertAction()
                .SubPageMovementAction();
        }
    }
}
