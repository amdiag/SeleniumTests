using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebDriver_CSharp_Example
{
    [TestFixture]
    public class Chrome_Sample_test
    {
        const string xpathCountrySelector = "//*[@id='country-element']//span[@class='selecter-selected']";
        private IWebDriver browser;
        public string homeURL;

        [Test(Description = "Check SauceLabs Homepage for Login Link")]
        public void Login_is_on_home_page()
        {
            browser.Navigate().GoToUrl(homeURL);
            browser.Manage().Window.Maximize();
            WebDriverWait wait = new WebDriverWait(browser, System.TimeSpan.FromSeconds(15));
            wait.Until(browser => browser.FindElement(By.XPath(xpathCountrySelector)));
            IWebElement CountrySelector = browser.FindElement(By.XPath(xpathCountrySelector));
            CountrySelector.Click();
            CountrySelector.

            /*WebDriverWait wait = new WebDriverWait(browser,
System.TimeSpan.FromSeconds(15));
            wait.Until(browser =>
browser.FindElement(By.XPath("//a[@href='/beta/login']")));
            IWebElement element =
browser.FindElement(By.XPath("//a[@href='/beta/login']"));
            Assert.AreEqual("Sign In", element.GetAttribute("text"));*/
        }


        [TearDown]
        public void TearDownTest()
        {
            browser.Close();
        }


        [SetUp]
        public void SetupTest()
        {
            homeURL = "https://careers.veeam.com/";
            browser = new ChromeDriver();
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15); 
        }
    }
}