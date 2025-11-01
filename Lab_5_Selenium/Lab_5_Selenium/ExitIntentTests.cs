using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class ExitIntentTests
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
        public void TestExitIntentModal()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/exit_intent");

            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h3[text()='Exit Intent']")));
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(
                "var event = new MouseEvent('mouseleave', {" +
                "    'view': window," +
                "    'bubbles': true," +
                "    'cancelable': true," +
                "    'clientY': -1" +
                "});" +
                "document.documentElement.dispatchEvent(event);" 
            );

            var modal = wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("ouibounce-modal")));
            
            Assert.That(modal.Displayed, Is.True, "Модальне вікно не з'явилося");

            var modalTitle = modal.FindElement(By.TagName("h3")).Text;
            Assert.That(modalTitle, Is.EqualTo("This is a modal window"), 
                "Неправильний заголовок у модальному вікні");

            var closeButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[@class='modal-footer']/p")));
    
            js.ExecuteScript("arguments[0].click();", closeButton);

            bool isGone = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.Id("ouibounce-modal")));
            
            Assert.That(isGone, Is.True, "Модальне вікно не закрилося");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}