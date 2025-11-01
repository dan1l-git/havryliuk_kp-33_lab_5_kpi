using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class DropdownTests
    {
        public IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new SafariDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestDropdownSelection()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");

            var dropdownElement = wait.Until(ExpectedConditions.ElementExists(
                By.Id("dropdown")));
            
            var selectElement = new SelectElement(dropdownElement);

            Assert.That(selectElement.SelectedOption.Text, 
                Is.EqualTo("Please select an option"),
                "Неправильна опція за замовчуванням");

            selectElement.SelectByText("Option 1");
            
            Assert.That(selectElement.SelectedOption.Text, 
                Is.EqualTo("Option 1"),
                "Option 1 не була вибрана");

            selectElement.SelectByValue("2");
            
            Assert.That(selectElement.SelectedOption.Text, 
                Is.EqualTo("Option 2"),
                "Option 2 не була вибрана");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}