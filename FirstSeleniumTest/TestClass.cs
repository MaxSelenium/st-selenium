using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTest
{
    [TestFixture]
    public class TestClass
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            //driver = new ChromeDriver();
            //driver = new EdgeDriver();

            FirefoxOptions options = new FirefoxOptions();
            //options.BrowserExecutableLocation = @"C:\FF45\firefox.exe";
            options.UseLegacyImplementation = false;

            //52.6.0
            //options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            //options.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //driver = new FirefoxDriver(options);
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Test1()
        {
            driver.Url = "http://www.ya.ru/";
            driver.FindElement(By.Id("text")).SendKeys("работает");
            driver.FindElement(By.ClassName("search2__button")).Click();
            wait.Until(ExpectedConditions.TitleContains("работает — Яндекс:"));
        }

        [Test]
        public void LoginLiteCart()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}