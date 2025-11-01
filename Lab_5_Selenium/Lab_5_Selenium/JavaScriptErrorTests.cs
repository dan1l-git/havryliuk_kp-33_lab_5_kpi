using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class JavaScriptErrorTests
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
        public void TestForJavaScriptErrorTextOnPage()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_error");

            var errorTextElement = wait.Until(ExpectedConditions.ElementExists(
                By.TagName("p")));

            Assert.That(errorTextElement.Text, 
                Does.Contain("This page has a JavaScript error in the onload event"), 
                "Очікуваний текст про помилку не знайдено на сторінці");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}