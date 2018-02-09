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
            driver = new EdgeDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
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