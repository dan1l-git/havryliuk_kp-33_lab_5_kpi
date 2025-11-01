using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class CheckboxTests
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
        public void TestCheckboxesStateToggle()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");
            
            var checkbox1 = wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//form[@id='checkboxes']/input[1]")));
            
            var checkbox2 = driver.FindElement(
                By.XPath("//form[@id='checkboxes']/input[2]"));
            
            Assert.That(checkbox1.Selected, Is.False, "Чекбокс 1 не мав бути вибраний");
            Assert.That(checkbox2.Selected, Is.True, "Чекбокс 2 мав бути вибраний");
            
            checkbox1.Click();
            checkbox2.Click();
            
            Assert.That(checkbox1.Selected, Is.True, "Чекбокс 1 мав стати вибраним");
            Assert.That(checkbox2.Selected, Is.False, "Чекбокс 2 не мав бути вибраним");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}