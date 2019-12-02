using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Threading;

namespace WebDriver_CSharp_Example
{
    [TestFixture]
    public class Chrome_Sample_test
    {
        const int timeToWaitSec = 15;
        const string xpathCountrySelector = "//*[@id='country-element']//*[@class='selecter-selected']";
        const string xpathCountrySelect = "//*[@id='country-element']//span[@data-value='Romania']";

        const string xpathLanguageSelector = "//div[@id='language']//span[@class='selecter-selected']";
        const string xpathLanguageSelect = "//div[@id='language']//*[text()[contains(.,'English')]]";

        const string xpathApplyBtn = "//a[text()[contains(.,'Apply')]]";
        const string xpathShowAllBtn = "//a[text()='Show all jobs']";
        const string xpathAllVacancies = "//*[contains(@class,'vacancies-blocks ')]//div[contains(@class,'vacancies-blocks-col')]";

        private IWebDriver browser;
        private WebDriverWait wait;
        public string homeURL;

       
        [TestCaseSource(typeof(MyDataClass), "TestCases")]
        [Test(Description = "Check quantity of vacancy")]
        public void Test_select_country_language_check_vacancy_on_page(string country, string language, int numVacancy)
        {
            browser.Navigate().GoToUrl(homeURL);
            wait = new WebDriverWait(browser, System.TimeSpan.FromSeconds(15));

            CheckAndClickCountry(country);
            CheckAndClickLang(language);
            CheckVacancy(numVacancy);

            Thread.Sleep(10000);
        }

        private void CheckAndClickCountry( string country) {
            wait.Until(browser => browser.FindElement(By.XPath(xpathCountrySelector)));
            browser.FindElement(By.XPath(xpathCountrySelector)).Click();            
            IWebElement CountrySelect = browser.FindElement(By.XPath(xpathCountrySelect));
            CountrySelect.Click();
        }
        private void CheckAndClickLang(string language) {
            wait.Until(browser => browser.FindElement(By.XPath(xpathLanguageSelector)));
            browser.FindElement(By.XPath(xpathLanguageSelector)).Click();            
            IWebElement LanguageSelect = browser.FindElement(By.XPath(xpathLanguageSelect));
            LanguageSelect.Click();

            IWebElement ApplyBtn = browser.FindElement(By.XPath(xpathApplyBtn));
            ApplyBtn.Click();
            var showAllBtn = browser.FindElement(By.XPath(xpathShowAllBtn));

            while (browser.FindElement(By.XPath(xpathShowAllBtn)).Displayed)
            {
                showAllBtn.Click();
                Thread.Sleep(2000);
            }
        }
        private void CheckVacancy( int numVacancy)
        {
            var vacancies = browser.FindElements(By.XPath(xpathAllVacancies));
            Assert.True(vacancies.Count == numVacancy, $"Quantity of vacancy isn't equals. Founded - {vacancies.Count}");
        }


        [TearDown]
        public void TearDownTest()
        {
            try
            {
                browser.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception while stopping chrome driver {e.Message}");
            }
            
        }


        [SetUp]
        public void SetupTest()
        {
            homeURL = "https://careers.veeam.com/";
            browser = new ChromeDriver();
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            browser.Manage().Window.Maximize();
        }
    }
}

public class MyDataClass
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData("Romania", "English", 29);
        }
    }
}