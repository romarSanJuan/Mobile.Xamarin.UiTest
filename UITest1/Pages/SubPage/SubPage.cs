using NUnit.Framework;
using System;
using System.Linq;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery,
    Xamarin.UITest.Queries.AppQuery>;

namespace Mobile.Xamarin.UiTest.Pages.SubPage
{
    public class SubPage : BasePage
    {
        #region Element List
              
        readonly Query subPageElementLocator01;
        readonly Query subPageElementLocator02;
        readonly Query subPageElementLocator03;

        #endregion

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Element_Locator"),
            iOS = x => x.Marked("Element_Locator"),
        };

        public SubPage()
        {
            subPageElementLocator01 = x => x.Marked("Element_Locator1");
            subPageElementLocator02 = x => x.Marked("Element_Locator2");
            subPageElementLocator03 = x => x.Marked("Element_Locator3");
        }

        /* Page Action Samples */
        public SubPage SubPagePageAction()
        {
            /* Capturing Screenshot */
            _app.Screenshot("Screenshot Description");

            /* Selecting an element */
            _app.Tap(subPageElementLocator01);

            /* Double tap on an element using the element coordinates */
            _app.DoubleTapCoordinates(100, 200);

            /* Double tap on an element */
            _app.DoubleTap(subPageElementLocator02);

            /* Selecting element with multiple index */
            _app.Tap(x => x.Marked("Element_List").Index(0));

            /* Clearing Text Field */
            _app.ClearText(subPageElementLocator01);

            /* Entering Text in Text Field */
            _app.EnterText(subPageElementLocator02, "Input Text Value");

            return this;
        }

        // Page Movement Samples
        public SubPage SubPageMovementAction()
        {
            /* Touch gesture that scrolls down to specific element location in page */
            _app.ScrollDownTo("Element_Locator", "ScrollView");

            /* Touch gesture that scrolls down to specific element location in page */
            _app.ScrollUpTo("Element_Locator", "ScrollView");

            /* Simulates a left-to-right or a right-to-left gesture swipe. */
            _app.SwipeLeftToRight();
            _app.SwipeRightToLeft();

            return this;
        }

        /* Page Assertion Samples */
        public SubPage SubPageAssertAction()
        {
            /* Asserting Element expected and actual value */
            Assert.AreEqual("Expected Value", _app.Query(subPageElementLocator03).First().Text);

            /* Asserting Element if not visible */
            Assert.NotNull(_app.Query(subPageElementLocator02).FirstOrDefault());

            /* Waiting for Element to be visible */
            _app.WaitForElement(subPageElementLocator01, timeout: TimeSpan.FromSeconds(10));

            /* Waiting for Element not to be visible */
            _app.WaitForNoElement(subPageElementLocator03, timeout: TimeSpan.FromSeconds(10));

            return this;
        }

        /* Using Global Features in Test Steps */
        public SubPage SubPageGlobalFeatures()
        {
            AssertElementValue(subPageElementLocator03,"My Expected Value");

            AssertElementsPresent(subPageElementLocator02);

            AssertElementsNotPresent(subPageElementLocator01);

            AssertElementToggleValue("Element Name", false);

            DragToCoordinates(subPageElementLocator03, subPageElementLocator02);

            WaitForNoElement(subPageElementLocator02);

            WaitForElement(subPageElementLocator01);

            return this;
        }
    }
}
