using Query = System.Func<Xamarin.UITest.Queries.AppQuery,
    Xamarin.UITest.Queries.AppQuery>;

namespace Mobile.Xamarin.UiTest.Pages.MainPage
{
    public class MainPage : BasePage
    {
        #region Element List

        // Element List
        // List all Elements available in the page
        // that will be used in test execution

        readonly Query mainPageElementLocator01;
        readonly Query mainPageElementLocator02;
        readonly Query mainPageElementLocator03;

        #endregion

        protected override PlatformQuery Trait => new PlatformQuery
        {
            // Add specific element locator representing the Page.
            // Before the test is executed in the page, the specific Element listed here will validated first.
            // If not found, the Test Execution is terminated and marked as failed

            Android = x => x.Marked("Element_Locator"),
            iOS = x => x.Marked("Element_Locator"),
        };

        public MainPage()
        {
            // List all element available inside the Page
            // and add their corresponding locators
            // Example: Xpath, Automation Id, Text and etc.

            mainPageElementLocator01 = x => x.Marked("Element_Locator1");
            mainPageElementLocator02 = x => x.Marked("Element_Locator2");
            mainPageElementLocator03 = x => x.Marked("Element_Locator3");
        }

        // Add user actions that can be done inside the Page
        public MainPage MainPageAction01()
        {
            _app.Tap(mainPageElementLocator01);

            return this;
        }

        public MainPage MainPageAction02()
        {
            // Add user actions
            _app.Tap(mainPageElementLocator02);

            return this;
        }

        // Add assert actions that can be done inside the Page
        public MainPage MainPageAssertion01()
        {
            _app.WaitForElement(mainPageElementLocator03);

            return this;
        }
    }
}
