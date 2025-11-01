using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class ShiftingContentTests
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
        public void TestMenuClickDespiteShift()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/shifting_content/menu");

            var portfolioLink = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.LinkText("Portfolio")));
            
            portfolioLink.Click();

            var pageTitleElement = wait.Until(ExpectedConditions.ElementExists(
                By.TagName("h1")));
            
            Assert.That(pageTitleElement.Text, 
                Is.EqualTo("Not Found"), 
                "Заголовок сторінки неправильний");
            
            Assert.That(driver.Url, Does.Contain("/portfolio/"), "URL не змінився");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}