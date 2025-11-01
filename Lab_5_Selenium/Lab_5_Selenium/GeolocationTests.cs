using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class GeolocationTests
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
        public void TestGeolocationRequest()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/geolocation");

            var findMeButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[@onclick='getLocation()']")));
            
            findMeButton.Click();
            
            var latitudeValue = wait.Until(ExpectedConditions.ElementExists(
                By.Id("lat-value")));

            var longitudeValue = wait.Until(ExpectedConditions.ElementExists(
                By.Id("long-value")));

            Assert.That(latitudeValue.Text, Is.Not.Empty, "Значення широти не з'явилося");
            Assert.That(longitudeValue.Text, Is.Not.Empty, "Значення довготи не з'явилося");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}