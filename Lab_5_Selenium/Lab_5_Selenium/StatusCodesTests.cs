using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class StatusCodesTests
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
        public void TestStatusCodesPages()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/status_codes");
            
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("200"))).Click();
            
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//p[contains(text(), 'This page returned a 200 status code')]")));
            
            Assert.That(driver.FindElement(By.TagName("body")).Text, 
                Does.Contain("This page returned a 200 status code"));
            driver.Navigate().Back();
            
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("404"))).Click();
            
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//p[contains(text(), 'This page returned a 404 status code')]")));
            
            Assert.That(driver.FindElement(By.TagName("body")).Text, 
                Does.Contain("This page returned a 404 status code"));
            driver.Navigate().Back();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("500"))).Click();
            
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//p[contains(text(), 'This page returned a 500 status code')]")));
            
            Assert.That(driver.FindElement(By.TagName("body")).Text, 
                Does.Contain("This page returned a 500 status code"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}