using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumTests
{
    [TestFixture]
    public class FrameTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/frames");
        }

        [Test]
        public void TestIFrame()
        {
            driver.FindElement(By.LinkText("iFrame")).Click();
            driver.SwitchTo().Frame("mce_0_ifr");

            // Wait until the editable area is visible
            wait.Until(driver => driver.FindElement(By.Id("tinymce")).Text.Length > 0);


            IWebElement contentArea = driver.FindElement(By.Id("tinymce"));
            string contentText = contentArea.Text;

            // Using Assert.That for the assertion
            Assert.That(contentText, Does.Contain("Your content goes here."), "The text in the iframe does not match the expected default text");

            driver.SwitchTo().DefaultContent();
            
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
