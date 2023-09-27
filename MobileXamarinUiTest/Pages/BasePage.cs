using NUnit.Framework;
using System;
using System.Linq;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery,
    Xamarin.UITest.Queries.AppQuery>;

namespace Mobile.Xamarin.UiTest
{
    public abstract class BasePage
    {
        protected IApp _app => AppManager.App;

        protected abstract PlatformQuery Trait { get; }

        protected BasePage()
        {
            AssertOnPage(TimeSpan.FromSeconds(10));
            _app.Screenshot("On " + this.GetType().Name);
        }

        /* Check if the test execution is on the proper page */
        protected void AssertOnPage(TimeSpan? timeout = default)
        {
            var message = "Unable to verify on page: " + this.GetType().Name;

            if (timeout == null)
                Assert.IsNotEmpty(_app.Query(Trait.Current), message);
            else
                Assert.DoesNotThrow(() => _app.WaitForElement(Trait.Current, timeout: timeout), message);
        }

        /* Page Transition, Check if test execution exit the previous page */
        protected void WaitForPageToLeave(TimeSpan? timeout = default)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(5);
            var message = "Unable to verify *not* on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => _app.WaitForNoElement(Trait.Current, timeout: timeout), message);
        }

        #region Additional reusable feature for all test

        /* Get and return Element Text with multiple index (e.g. Dropdown Menu, List Items) */
        public string GetElementTextwithIndex(string elementLocator, int index)
        {
            // Element Locator and specific index
            string elementValue = _app.Query(c => c.Marked(elementLocator))[index].Text;

            return elementValue;
        }

        /* Asserting Element Text - Comparing Actual and Expected Value */
        public void AssertElementValue(string expectedValue, Query elementLocator)
        {
            var message = "Incorrect expected value on element " + elementLocator + " expected " + expectedValue;

            Assert.AreEqual(expectedValue, _app.Query(elementLocator).First().Text, message);
        }

        /* Assert if Element is displayed on the screen */
        public void AssertElementsPresent(Query elementLocator)
        {
            var message = "Unable to verify element present " + elementLocator;

            Assert.NotNull(_app.Query(elementLocator).FirstOrDefault(), message);
        }

        /* Assert if Element is not displayed on the screen */
        public void AssertElementsNotPresent(Query elementLocator)
        {
            var message = "Unable to verify element not present " + elementLocator;

            Assert.IsEmpty(_app.Query(elementLocator), message);
            Assert.IsNull(_app.Query(elementLocator), message);
        }

        /* Assert Toggle Element State */
        public void AssertElementToggleValue(string elementLocator, bool elementState)
        {
            bool isChecked;

            if (elementState == true)
            {
                isChecked = (bool)_app.Query(c => c.Switch(elementLocator).Invoke("isChecked"))?.FirstOrDefault();
                Assert.IsTrue(isChecked);
            }
            else if (elementState == false)
            {
                isChecked = (bool)_app.Query(c => c.Switch(elementLocator).Invoke("isChecked"))?.FirstOrDefault();
                Assert.IsFalse(isChecked);
            }
        }

        /* Drag and Dropping from first element location to second element location */
        public void DragToCoordinates(Query firstElement, Query secondElement)
        {
            var elementOne = _app.Query(firstElement).First();
            var elementOneX = elementOne.Rect.X;
            var elementOneY = elementOne.Rect.Y;
            var elementTwo = _app.Query(secondElement).First();
            var elemenTwoX = elementTwo.Rect.X;
            var elemenTwoY = elementTwo.Rect.Y;
            _app.DragCoordinates(elementOneX, elementOneY, elemenTwoX, elemenTwoY);
        }

        /* Wait for Element to not display on the active screen for specific Time Span */
        public void WaitForNoElement(Query elementLocator)  
        {
            var message = "Unable to verify Wait for no Element " + elementLocator;
            _app.WaitForNoElement(elementLocator, message, timeout: TimeSpan.FromSeconds(10));
        }

        /* Wait for Element to display on the active screen for specific Time Span */
        public void WaitForElement(Query elementLocator)
        {
            var message = "Unable to verify Wait for Element " + elementLocator;
            _app.WaitForElement(elementLocator, message, timeout: TimeSpan.FromSeconds(10));
        }

        #endregion
    }
}
