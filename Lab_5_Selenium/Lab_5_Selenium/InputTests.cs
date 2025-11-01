using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class InputsTests
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
        public void TestNumberInputInteraction()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/inputs");

            var inputElement = wait.Until(ExpectedConditions.ElementExists(
                By.CssSelector("input[type='number']")));

            inputElement.SendKeys("123");
            Assert.That(inputElement.GetAttribute("value"), 
                Is.EqualTo("123"),
                "Цифри не були введені коректно");

            inputElement.SendKeys(Keys.ArrowUp);
            Assert.That(inputElement.GetAttribute("value"), 
                Is.EqualTo("124"),
                "Стрілка вгору не спрацювала");
            
            inputElement.SendKeys(Keys.ArrowDown);
            Assert.That(inputElement.GetAttribute("value"), 
                Is.EqualTo("123"),
                "Стрілка вниз не спрацювала");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}