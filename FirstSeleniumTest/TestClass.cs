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
using OpenQA.Selenium.Support.Events;
using System.Globalization;
using System.IO;
using FirstSeleniumTest.Properties;
using OpenQA.Selenium.Remote;

namespace FirstSeleniumTest
{
    [TestFixture]
    public class TestClass
    {
        //private IWebDriver driver;
        private EventFiringWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new EventFiringWebDriver(new ChromeDriver());
            //driver = new ChromeDriver();
            //driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome());
            //driver = new EdgeDriver();
            //driver = new FirefoxDriver();

            //FirefoxOptions options = new FirefoxOptions();
            //options.BrowserExecutableLocation = @"C:\FF45\firefox.exe";
            //options.UseLegacyImplementation = true;

            //52.6.0
            //options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            //options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            //options.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            //driver = new FirefoxDriver(options);
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(5);
            //driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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

            int countItem = driver.FindElements(By.CssSelector(".product")).Count();
            for (int i = 0; i < countItem; i++)
            {
                int s = driver.FindElements(By.CssSelector(".product"))[i].FindElements(By.CssSelector("div[class^='sticker']")).Count();
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
            Product main = new Product();
            main.Name = mainItem.FindElement(By.CssSelector("div.name")).Text;           
            main.Price = mainItem.FindElement(By.CssSelector("s.regular-price")).Text;
            main.PriceFront = GetFront(mainItem.FindElement(By.CssSelector("s.regular-price")));
            main.SalePrice = mainItem.FindElement(By.CssSelector("strong.campaign-price")).Text;
            main.SalePriceFront = GetFront(mainItem.FindElement(By.CssSelector("strong.campaign-price")));
            if (!IsGray(main.PriceFront.Color))
                Assert.Fail($"Главная страница: Основная цена не серая");
            if (!IsRed(main.SalePriceFront.Color))
                Assert.Fail($"Главная страница: Скидочная цена не красная");           
            if (!main.PriceFront.through)
                Assert.Fail($"Главная страница: Основная цена не зачеркнута!");
            if (!main.SalePriceFront.bold)
                Assert.Fail($"Главная страница: Скидочная цена не выделена жирным!");
            if (!(main.PriceFront.Size < main.SalePriceFront.Size))
                Assert.Fail($"Главная страница: Основная цена не меньше скидочной");

            mainItem.Click();

            Product product = new Product();
            var productItem = driver.FindElement(By.CssSelector("div#box-product"));
            product.Name = productItem.FindElement(By.CssSelector("h1.title")).Text;
            product.Price = productItem.FindElement(By.CssSelector("s.regular-price")).Text;
            product.PriceFront = GetFront(productItem.FindElement(By.CssSelector("s.regular-price")));
            product.SalePrice = productItem.FindElement(By.CssSelector("strong.campaign-price")).Text;
            product.SalePriceFront = GetFront(productItem.FindElement(By.CssSelector("strong.campaign-price")));

            if(!IsGray(product.PriceFront.Color))
                Assert.Fail($"Страница товара: Основная цена не серая");
            if (!IsRed(product.SalePriceFront.Color))
                Assert.Fail($"Страница товара: Скидочная цена не красная");
            if (main.Name != product.Name)
                Assert.Fail($"Не совпадает имя с главной страницей!");
            else if (main.Price != product.Price)
                Assert.Fail($"Не совпадает цена с главной страницей!");
            else if (main.SalePrice != product.SalePrice)
                Assert.Fail($"Не совпадает скидочная цена с главной страницей!");
            if (!product.PriceFront.through)
                Assert.Fail($"Страница товара: Основная цена не зачеркнута!");
            if (!product.SalePriceFront.bold)
                Assert.Fail($"Страница товара: Скидочная цена не выделена жирным!");
            if (!(product.PriceFront.Size < product.SalePriceFront.Size))
                Assert.Fail($"Страница товара: Основная цена не меньше скидочной");
        }


        [Test]
        public void Task11()
        {
            driver.Url = "http://localhost/litecart/en/";
            var logForm = driver.FindElement(By.Name("login_form"));
            //logForm.FindElement(By.CssSelector("[href]")).Click();
            logForm.FindElement(By.XPath(".//a[contains(.,'New customers click here')]")).Click();

            driver.FindElement(By.Name("tax_id")).SendKeys("123");
            driver.FindElement(By.Name("company")).SendKeys("VSK");
            driver.FindElement(By.Name("firstname")).SendKeys("Maxim");
            driver.FindElement(By.Name("lastname")).SendKeys("Bokov");
            driver.FindElement(By.Name("address1")).SendKeys("address1");
            driver.FindElement(By.Name("address2")).SendKeys("address2");
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).SendKeys("Moscow");
            var select = driver.FindElement(By.Name("country_code"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("arguments[0].selectedIndex=6; arguments[0].dispatchEvent(new Event('change'))", select);
            js.ExecuteScript("arguments[0].style.opacity=1", select);
            SelectElement s = new SelectElement(select);
            s.SelectByText("United States");

            string email = GetEmail();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("79269266699");
            driver.FindElement(By.Name("password")).SendKeys("123");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("123");
            driver.FindElement(By.Name("create_account")).Click();
            select = driver.FindElement(By.Name("zone_code"));
            js.ExecuteScript("arguments[0].style.opacity=1", select);
            s = new SelectElement(select);
            s.SelectByText("California");
            driver.FindElement(By.Name("password")).SendKeys("123");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("123");
            driver.FindElement(By.Name("create_account")).Click();
            var menuAcc = driver.FindElement(By.Id("box-account"));
            menuAcc.FindElement(By.XPath(".//a[contains(.,'Logout')]")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys("123");
            driver.FindElement(By.Name("login")).Click();
            menuAcc = driver.FindElement(By.Id("box-account"));
            menuAcc.FindElement(By.XPath(".//a[contains(.,'Logout')]")).Click();
        }

        [Test]
        public void Task12()
        {
            Login();
            driver.FindElement(By.XPath("//span[contains(@class,'name') and .='Catalog']")).Click();
            driver.FindElement(By.XPath("//a[contains(@class,'button') and .=' Add New Product']")).Click();
           // driver.FindElement(By.XPath("//a[.='General']")).Click();
            driver.FindElement(By.XPath("//label[.=' Enabled']")).Click();
            driver.FindElement(By.XPath("//input[contains(@name,'name')]")).SendKeys("Prod");
            driver.FindElement(By.XPath("//input[@name='code']")).SendKeys("123");
            driver.FindElement(By.XPath("//input[contains(@data-name,'Rubber Ducks')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@name,'product_groups') and (@value='1-1')]")).Click();
            driver.FindElement(By.XPath("//input[@name='quantity']")).Clear();
            driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys("12");
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "prod.jpg");
            driver.FindElement(By.XPath("//input[contains(@name,'new_images')]")).SendKeys(path);
            driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("01012018");
            driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("01032018");
            
            driver.FindElement(By.XPath("//a[.='Information']")).Click(); 
            var select= new SelectElement(driver.FindElement(By.CssSelector("select[name='manufacturer_id']")));
            select.SelectByValue("1");
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("product");
            driver.FindElement(By.XPath("//input[contains(@name,'short_description')]")).SendKeys("Short Description");
            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys(@"Описание очень хорошего товара
Нужно брать!");
            driver.FindElement(By.XPath("//input[contains(@name,'head_title')]")).SendKeys("Head Title");
            driver.FindElement(By.XPath("//input[contains(@name,'meta_description')]")).SendKeys("Meta Description");
            driver.FindElement(By.XPath("//a[.='Prices']")).Click();
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).Clear();
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys("799,99");
            select = new SelectElement(driver.FindElement(By.CssSelector("select[name='purchase_price_currency_code']")));
            select.SelectByText("Euros");
            var price = driver.FindElements(By.XPath("//input[contains(@name,'prices')]"));
            price[0].SendKeys("20");
            price[2].SendKeys("40");
            driver.FindElement(By.XPath("//button[@name='save']")).Click();
        }


        [Test]
        public void Task13()
        {
            driver.Url = "http://localhost/LiteCart2/en/";
            for (int i = 0; i < 3; i++)
            {
                driver.FindElements(By.XPath("//li[@class='product column shadow hover-light']"))[0].Click();
                
                if(IsExistsPresent(By.ClassName("options")))
                {
                    var select = new SelectElement(driver.FindElement(By.XPath("//select[contains(@name,'Size')]")));
                    select.SelectByValue("Small");
                }
                var element = driver.FindElement(By.CssSelector("span.quantity"));
                driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(element,(i + 1).ToString()));
                driver.FindElement(By.CssSelector("div#logotype-wrapper")).Click();
            }
            driver.FindElement(By.XPath("//a[.='Checkout »']")).Click();

            while (!IsExistsPresent(By.XPath("//a[.='<< Back']")))
            {
                if (IsExistsPresent(By.CssSelector("li.shortcut")))
                {
                    driver.FindElement(By.CssSelector("li.shortcut")).Click();
                }
                var elem = driver.FindElement(By.XPath("//table[@class='dataTable rounded-corners']"));
                driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
                wait.Until(ExpectedConditions.StalenessOf(elem));
            }
        }

        [Test]
        public void Task14()
        {
            Login();
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            string mainUrl = driver.Url.Substring(0,16);
            driver.FindElement(By.XPath("//a[contains(@class,'button') and .=' Add New Country']")).Click();
            var Links = driver.FindElements(By.CssSelector("td#content table [target='_blank']"));
            string mainWindow = driver.CurrentWindowHandle;
            ICollection<string> oldWindows = driver.WindowHandles;
            foreach (IWebElement link in Links)
            {
                link.Click();
                string newWin = wait.Until(anyWindowsOtherThan(oldWindows));  // ожидание появления нового окна,
                driver.SwitchTo().Window(newWin);
                if (mainUrl == driver.Url.Substring(0, 16)) Assert.Fail($"Переход на тот же ресурс!");
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
        }

        [Test]
        public void Task17()
        {
            Login();
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            int i = 1;
            int rowsCount = 2;
            while (i < rowsCount)
            {
                IWebElement tabl = driver.FindElement(By.CssSelector("[name='catalog_form']"));
                var rows = tabl.FindElements(By.CssSelector("tr.row"));
                rowsCount = rows.Count();
                bool isProd = false;
                var cells = rows[i].FindElements(By.TagName("td"));
                isProd = IsExistsPresent(cells[2], By.XPath(".//a[contains(@href,'product')]"));
                cells[2].FindElement(By.CssSelector("a")).Click();
                if (isProd)
                {
                    if (driver.Manage().Logs.GetLog("browser").Count > 0)
                    {
                        Console.WriteLine("При загрузки страницы возникли следующие ошибки:");
                        foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                        {
                            Console.WriteLine(l);
                        }
                        Assert.Fail("При загрузки страницы возникли ошибки");
                    }
                    driver.Navigate().Back();
                }
                i++;
            }
        }


        public Func<IWebDriver, string> anyWindowsOtherThan(ICollection<string> oldWindows)
        {
            List<string> handles = new List<string>(driver.WindowHandles);
            foreach (string h in oldWindows)
            {
                handles.Remove(h);
            }
            Func<IWebDriver, string> a =  delegate (IWebDriver I) {return handles.Count() > 0 ? handles.First() : null;};
            return a;
        }

        private string GetEmail()
        {
            return $"e{DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace(".", "")}@mail.ru";
        }
        private Front GetFront(IWebElement element)
        {
            Front f = new Front();
            f.Color = element.GetCssValue("color");
            f.Size = Convert.ToDecimal(element.GetCssValue("font-size").Replace("px","").Replace(".",","));
            f.through = element.GetCssValue("text-decoration").Contains("line-through");
            int weight = Convert.ToInt32(element.GetCssValue("font-weight"));
            if(weight>699)
                f.bold = true;
            else
                f.bold = false;
            return f;
        }

        private bool IsGray(string color)
        {
            var c = color.Replace("rgba(","").Replace(")","").Replace(" ","").Replace("rgb(","").Split(',');
            if(c[0] == c[1] && c[1] == c[2])
                    return true;
            return false;
        }

        private bool IsRed(string color)
        {
            var c = color.Replace("rgba(", "").Replace(")", "").Replace(" ", "").Replace("rgb(", "").Split(',');
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

        private bool IsExists(IWebElement elem, By locator)
        {
            return elem.FindElements(locator).Count > 0;
        }

        private bool IsExistsPresent(By locator)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(0);
                return driver.FindElements(locator).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
        }
        private bool IsExistsPresent(IWebElement elem, By locator)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                return elem.FindElements(locator).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
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