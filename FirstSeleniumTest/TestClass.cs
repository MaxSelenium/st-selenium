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
            driver = new ChromeDriver();
            //driver = new EdgeDriver();

            //FirefoxOptions options = new FirefoxOptions();
            //options.BrowserExecutableLocation = @"C:\FF45\firefox.exe";
            //options.UseLegacyImplementation = true;

            //52.6.0
            //options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            //options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            //options.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            //driver = new FirefoxDriver(options);
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
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

        [Test]
        public void AdminPart()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            int countItemLevel1 = driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-")).Count();
            for (int i = 0; i < countItemLevel1; i++)
            {
                driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-"))[i].Click();
                
               
                if  (isExists(By.CssSelector("li#app-[class='selected'] ul.docs")))
                {
                    Compare(driver.FindElement(By.CssSelector("h1")).Text, driver.FindElement(By.CssSelector("li#app- li.selected")).Text);
                    int countItemLevel2 = driver.FindElements(By.CssSelector("li#app-[class='selected'] ul.docs [href ^='http://lo']")).Count();
                    for (int n = 0; n < countItemLevel2; n++)
                    {
                        driver.FindElements(By.CssSelector("li#app-[class='selected'] ul.docs [href ^='http://lo']"))[n].Click();

                        Compare(driver.FindElement(By.CssSelector("h1")).Text,driver.FindElement(By.CssSelector("li#app- li.selected")).Text);
                    }
                }
                else
                    Compare(driver.FindElement(By.CssSelector("h1")).Text, driver.FindElement(By.CssSelector("li#app-[class='selected']")).Text);
            }
            
        }

        [Test]
        public void Task7()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            int countItemLevel1 = driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-")).Count();
            for (int i = 0; i < countItemLevel1; i++)
            {
                driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-"))[i].Click();
                Assert.True(isExists(By.CssSelector("h1")));
                if (isExists(By.CssSelector("li#app-[class='selected'] ul.docs")))
                {
                    
                    int countItemLevel2 = driver.FindElements(By.CssSelector("li#app-[class='selected'] ul.docs [href ^='http://lo']")).Count();
                    for (int n = 0; n < countItemLevel2; n++)
                    {
                        driver.FindElements(By.CssSelector("li#app-[class='selected'] ul.docs [href ^='http://lo']"))[n].Click();
                        Assert.True(isExists(By.CssSelector("h1")));
                    }
                }
            }
        }
        private void Compare (string text1, string text2)
        {
            if (text1 != text2)
                Assert.Fail($"Страница {text1} не равна меню {text2} !");
        }
        private bool isExists( By locator  )
        {
            return driver.FindElements(locator).Count > 0;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}