using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class AddRemoveElementsTests
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
        public void TestAddAndRemoveElement()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/add_remove_elements/");
            
            var addButton = wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//button[@onclick='addElement()']")));
            
            wait.Until(ExpectedConditions.ElementToBeClickable(addButton));
            
            addButton.Click();

            var deleteButton = wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//button[@onclick='deleteElement()']")));
            
            Assert.That(deleteButton.Displayed, Is.True);

            deleteButton.Click();

            bool isGone = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath("//button[@onclick='deleteElement()']")));

            Assert.That(isGone, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}