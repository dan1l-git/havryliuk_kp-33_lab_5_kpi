using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab_5_Selenium
{
    [TestFixture]
    public class DragAndDropTests
    {
        public IWebDriver driver;
        private WebDriverWait wait;

        private const string DragDropScript = @"
            function simulateDragDrop(sourceNode, destinationNode) {
                var EVENT_TYPES = {
                    DRAG_START: 'dragstart',
                    DRAG_ENTER: 'dragenter',
                    DRAG_OVER: 'dragover',
                    DROP: 'drop',
                    DRAG_END: 'dragend'
                };

                function createCustomEvent(type) {
                    var event = new CustomEvent(type, {bubbles: true, cancelable: true});
                    event.dataTransfer = {
                        data: {},
                        setData: function(type, val) {
                            this.data[type] = val;
                        },
                        getData: function(type) {
                            return this.data[type];
                        }
                    };
                    return event;
                }

                var dragStartEvent = createCustomEvent(EVENT_TYPES.DRAG_START);
                sourceNode.dispatchEvent(dragStartEvent);

                var dragEnterEvent = createCustomEvent(EVENT_TYPES.DRAG_ENTER);
                destinationNode.dispatchEvent(dragEnterEvent);

                var dragOverEvent = createCustomEvent(EVENT_TYPES.DRAG_OVER);
                destinationNode.dispatchEvent(dragOverEvent);

                var dropEvent = createCustomEvent(EVENT_TYPES.DROP);
                dropEvent.dataTransfer = dragStartEvent.dataTransfer;
                destinationNode.dispatchEvent(dropEvent);

                var dragEndEvent = createCustomEvent(EVENT_TYPES.DRAG_END);
                dragEndEvent.dataTransfer = dragStartEvent.dataTransfer;
                sourceNode.dispatchEvent(dragEndEvent);
            }
            simulateDragDrop(arguments[0], arguments[1]);
        ";

        [SetUp]
        public void Setup()
        {
            driver = new SafariDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestDragAndDrop()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/drag_and_drop");

            var elementA = wait.Until(ExpectedConditions.ElementExists(By.Id("column-a")));
            var elementB = wait.Until(ExpectedConditions.ElementExists(By.Id("column-b")));

            Assert.That(elementA.FindElement(By.TagName("header")).Text, Is.EqualTo("A"));
            Assert.That(elementB.FindElement(By.TagName("header")).Text, Is.EqualTo("B"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(DragDropScript, elementA, elementB);

            Assert.That(elementA.FindElement(By.TagName("header")).Text, 
                Is.EqualTo("B"), 
                "Елемент A мав отримати заголовок 'B' після перетягування");
            Assert.That(elementB.FindElement(By.TagName("header")).Text, 
                Is.EqualTo("A"), 
                "Елемент B мав отримати заголовок 'A' після перетягування");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}