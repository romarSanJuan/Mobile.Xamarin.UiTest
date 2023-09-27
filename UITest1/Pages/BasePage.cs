using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
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

        #region Page Transition (POM)

        /// <summary>
        /// POM implementation that wait and verifies if test execution is on the expected page.
        /// </summary>
        protected void AssertOnPage(TimeSpan? timeout = default)
        {
            var message = "Unable to verify on page: " + this.GetType().Name;

            if (timeout == null)
                Assert.IsNotEmpty(_app.Query(Trait.Current), message);
            else
                Assert.DoesNotThrow(() => _app.WaitForElement(Trait.Current, timeout: timeout), message);
        }

        /// <summary>
        /// POM implementation that wait and verifies if test execution exits the previous page.
        /// </summary>
        protected void WaitForPageToLeave(TimeSpan? timeout = default)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(5);
            var message = "Unable to verify *not* on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => _app.WaitForNoElement(Trait.Current, timeout: timeout), message);
        }

        #endregion

        #region Data Function

        /// <summary>
        /// Data function that extract value text of element with multiple index (e.g. Dropdown Menu List Items).
        /// </summary>
        public string GetElementTextwithIndex(string elementLocator, int index)
        {
            string elementValue = _app.Query(elementLocator)[index].Text;

            return elementValue;
        }

        /// <summary>
        /// Data function that clears the element input field and enterr a value text.
        /// </summary>
        public void InputValue(Query elementLocator, string value)
        {
            _app.ClearText(elementLocator);
            _app.EnterText(elementLocator, value);
        }

        /// <summary>
        /// Data function that selects a value from List Item by index number.
        /// </summary>
        public void SelectListItemByIndex(string elementLocator, int index)
        {
            _app.Tap(x => x.Marked(elementLocator).Index(index));
        }

        /// <summary>
        /// Data function that selects a value from List Item by value text.
        /// </summary>
        public void SelectListItemByText(string elementLocator, string value)
        {
            _app.Tap(x => x.Marked(elementLocator).Text(value));
        }

        #endregion

        #region Assert Functions

        /// <summary>
        /// Assert function that verifies the element actual value is equal to test expected value.
        /// </summary>
        public void AssertElementValue(Query elementLocator, string expectedValue)
        {
            var message = "Incorrect expected value on element " + elementLocator + " expected " + expectedValue;

            Assert.AreEqual(expectedValue, _app.Query(elementLocator).First().Text, message);
        }

        /// <summary>
        /// Assert function that verifies the element text is PRESENT on the screen.
        /// </summary>
        public void AssertElementsPresent(Query elementLocator)
        {
            var message = "Unable to verify element present " + elementLocator;

            Assert.NotNull(_app.Query(elementLocator).FirstOrDefault(), message);
        }

        /// <summary>
        /// Assert function that verifies the element text is NOT PRESENT on the screen.
        /// </summary>
        public void AssertElementsNotPresent(Query elementLocator)
        {
            var message = "Unable to verify element not present " + elementLocator;

            Assert.IsEmpty(_app.Query(elementLocator), message);
            Assert.IsNull(_app.Query(elementLocator), message);
        }

        /// <summary>
        /// Assert function that verifies toggle element state.
        /// </summary>
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

        #endregion

        #region Action Functions

        /// <summary>
        /// Action function that will drag and drop from first element location to second element location.
        /// </summary>
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

        #endregion

        #region Wait Functions

        /// <summary>
        /// Wait function that will repeatly query the app until a matching element is NOT PRESENT on the screen.
        /// Throws a System.TimeoutException if no element is found within the time limit.
        /// </summary>
        public void WaitForNoElement(Query elementLocator)  
        {
            var message = "Unable to verify Wait for no Element " + elementLocator;
            _app.WaitForNoElement(elementLocator, message, timeout: TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Wait function that will repeatly query the app until a matching element is PRESENT on the screen.
        /// Throws a System.TimeoutException if no element is found within the time limit.
        /// </summary>
        public void WaitForElement(Query elementLocator)
        {
            var message = "Unable to verify Wait for Element " + elementLocator;
            _app.WaitForElement(elementLocator, message, timeout: TimeSpan.FromSeconds(10));
        }

        #endregion

        #region Random Functions

        /// <summary>
        /// Random Functions that will get the element List Item count.
        /// </summary>
        public int GetListItemCount(Query elementLocator)
        {
            int count = _app.Query(elementLocator).Length;

            return count;
        }

        /// <summary>
        /// Random Functions that will get a rendom integer between min and max value.
        /// </summary>
        public int GetRandomInteger(int min, int max)
        {
            Random random = new Random();
            int randomSelection = random.Next(min, max);

            return randomSelection;
        }

        /// <summary>
        /// Random Functions that will select a random value from List Item aside from the default value.
        /// Note: (Expectation) Default value is assign on List Item Index 0
        /// </summary>
        public void SelectRandomListItem(Query elementLocator)
        {
            int listCount = GetListItemCount(elementLocator);
            int randomFilterSelected = 0;
            int counter = 0;          
            
            while (counter <= 0)
            {
                randomFilterSelected = GetRandomInteger(0, listCount);
                if (randomFilterSelected > 0)
                {
                    counter++;
                }
            }
            _app.Tap(_app.Query(elementLocator)[randomFilterSelected].Text);
        }

        /// <summary>
        /// Random Functions that will select a random value from Slider Element.
        /// Note: (Expectation) Min and Max Value (Text) displayed on the sceen. Select random between min and maximum slider value
        /// </summary>
        public void SelectSliderValue(Query sliderMinValue, Query sliderMaxValue, Query elementLocator, int value)
        {
            int minValue = Int32.Parse(_app.Query(sliderMinValue).First().Text);
            int maxValue = Int32.Parse(_app.Query(sliderMaxValue).First().Text);
            int randomValue = GetRandomInteger(minValue, maxValue);
            _app.SetSliderValue(elementLocator, randomValue);
        }

        #endregion
    }
}
