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
            Login();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
        }

        [Test]
        public void AdminPart()
        {
            Login();
            int countItemLevel1 = driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-")).Count();
            for (int i = 0; i < countItemLevel1; i++)
            {
                driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-"))[i].Click();
                
               
                if  (IsExists(By.CssSelector("li#app-[class='selected'] ul.docs")))
                {
                    Compare(driver.FindElement(By.CssSelector("h1")).Text, driver.FindElement(By.CssSelector("li#app- li.selected")).Text);
                    int countItemLevel2 = driver.FindElements(By.CssSelector("[class='selected'] ul.docs [href]")).Count();
                    for (int n = 0; n < countItemLevel2; n++)
                    {
                        driver.FindElements(By.CssSelector("[class='selected'] ul.docs [href]"))[n].Click();

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
            Login();
            int countItemLevel1 = driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-")).Count();
            for (int i = 0; i < countItemLevel1; i++)
            {
                driver.FindElements(By.CssSelector("div#box-apps-menu-wrapper li#app-"))[i].Click();
                Assert.True(IsExists(By.CssSelector("h1")));
                if (IsExists(By.CssSelector("li#app-[class='selected'] ul.docs")))
                {
                    
                    int countItemLevel2 = driver.FindElements(By.CssSelector("[class='selected'] ul.docs [href]")).Count();
                    for (int n = 0; n < countItemLevel2; n++)
                    {
                        driver.FindElements(By.CssSelector("[class='selected'] ul.docs [href]"))[n].Click();
                        Assert.True(IsExists(By.CssSelector("h1")));
                    }
                }
            }
        }


        [Test]
        public void Task8()
        {
            driver.Url = "http://localhost/litecart/en/";

            int countItem = driver.FindElements(By.CssSelector("li[class='product column shadow hover-light']")).Count();
            for (int i = 0; i < countItem; i++)
            {
                int s = driver.FindElements(By.CssSelector("li[class='product column shadow hover-light']"))[i].FindElements(By.CssSelector("div[class^='sticker']")).Count();
                Assert.True(s==1); 
            }
        }

        [Test]
        public void Task9()
        {
            Login();

            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            IWebElement tabl = driver.FindElement(By.CssSelector("[name='countries_form']"));
            var rows = tabl.FindElements(By.CssSelector("tr.row"));
            List<string> countries = new List<string>();
            List<int> attach = new List<int>();
          
            for (int i = 0;i<rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.TagName("td"));
                //if(cells[4].Text!="")
                    countries.Add(cells[4].Text);
                if (cells[5].Text!="0")
                    attach.Add(i);
            }
            if(!IsSorted(countries))
            {
                Assert.Fail($"Список стран не отсортирован!");
            }
            foreach (int i in attach)
            {
                driver.FindElement(By.CssSelector("[name='countries_form']")).FindElements(By.CssSelector("tr.row"))[i].FindElement(By.CssSelector("[href]")).Click();
                IWebElement tabl2 = driver.FindElement(By.CssSelector("[id='table-zones']"));
                var rows2 = tabl2.FindElements(By.CssSelector("tr:not([class='header'])"));
                List<string> zones = new List<string>();
                for (int  n= 0; n < rows2.Count; n++)
                {
                    var cells = rows2[n].FindElements(By.TagName("td"));
                    if(cells[2].Text!="")
                        zones.Add(cells[2].Text);
                }
                if (!IsSorted(zones))
                {
                    Assert.Fail($"Список зон не отсортирован!");
                }
              driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            }
        }
        [Test]
        public void Task9_2()
        {
            Login();

            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            IWebElement tabl = driver.FindElement(By.CssSelector("[name='geo_zones_form']"));
            var rows = tabl.FindElements(By.CssSelector("tr.row"));
            List<string> countries = new List<string>();
            for (int i = 0; i < rows.Count; i++)
            {
                driver.FindElement(By.CssSelector("[name='geo_zones_form']")).FindElements(By.CssSelector("tr.row"))[i].FindElement(By.CssSelector("[href]")).Click();
                IWebElement tabl2 = driver.FindElement(By.CssSelector("[id='table-zones']"));
                var rows2 = tabl2.FindElements(By.CssSelector("tr:not([class='header'])"));
                List<string> zones = new List<string>();
                for (int n = 0; n < rows2.Count; n++)
                {
                    var cells = rows2[n].FindElements(By.TagName("td"));
                    if (cells.Count>1)
                        zones.Add(cells[2].FindElement(By.CssSelector("[selected = 'selected']")).Text);
                }
                if (!IsSorted(zones))
                {
                    Assert.Fail($"Список зон не отсортирован!");
                }
                driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            }
        }

        [Test]
        public void Task10()
        {
            driver.Url = "http://localhost/litecart/en/";

            var mainItem = driver.FindElement(By.CssSelector("div#box-campaigns")).FindElement(By.CssSelector("li[class='product column shadow hover-light']"));
            Product Main = new Product();
            Main.Name = mainItem.FindElement(By.CssSelector("div.name")).Text;
            
            Main.Price = mainItem.FindElement(By.CssSelector("s.regular-price")).Text;
            Main.PriceColor = mainItem.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            Main.SalePrice = mainItem.FindElement(By.CssSelector("strong.campaign-price")).Text;
            Main.SalePriceColor = mainItem.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            if (!IsGray(Main.PriceColor))
                Assert.Fail($"Основная цена не серая");
            if (!IsRed(Main.SalePriceColor))
                Assert.Fail($"Скидочная цена не красная");
            mainItem.Click();
            Product product = new Product();
            var productItem = driver.FindElement(By.CssSelector("div#box-product"));
            product.Name = productItem.FindElement(By.CssSelector("h1.title")).Text;
            product.Price = productItem.FindElement(By.CssSelector("s.regular-price")).Text;
            product.PriceColor = productItem.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            product.SalePrice = productItem.FindElement(By.CssSelector("strong.campaign-price")).Text;
            product.SalePriceColor = productItem.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");

            if(!IsGray(product.PriceColor))
                Assert.Fail($"Основная цена не серая");
            if (!IsRed(product.SalePriceColor))
                Assert.Fail($"Скидочная цена не красная");
            if (Main.Name != product.Name)
                Assert.Fail($"Не совпадает имя с главной страницей!");
            else if (Main.Price != product.Price)
                Assert.Fail($"Не совпадает цена с главной страницей!");
            else if (Main.SalePrice != product.SalePrice)
                Assert.Fail($"Не совпадает скидочная цена с главной страницей!");
            //for (int i = 0; i < countItem; i++)
            //{
            //    int s = driver.FindElements(By.CssSelector("div.middle li[class='product column shadow hover-light']"))[i].FindElements(By.CssSelector("div[class^='sticker']")).Count();
            //    Assert.True(s == 1);
            //}
        }

        private bool IsGray(string color)
        {
            var c = color.Replace("rgba(","").Replace(")","").Replace(" ","").Split(',');
            if(c[0] == c[1] && c[1] == c[2])
                    return true;
            return false;
        }

        private bool IsRed(string color)
        {
            var c = color.Replace("rgba(", "").Replace(")", "").Replace(" ", "").Split(',');
            if (c[1] == "0" && c[2]=="0")
                return true;
            return false;
        }
        private bool IsSorted (List<string> list1)
        {
            List<string> list2 = new List<string>(list1);
            list1.Sort();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                    return false;
            }
            return true;
        }
        private void Compare (string text1, string text2)
        {
            if (text1 != text2)
                Assert.Fail($"Страница {text1} не равна меню {text2} !");
        }
        private bool IsExists(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }
        private void Login ()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}