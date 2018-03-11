using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstSeleniumTest.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTest.Manager
{
    public class Application
    {
        
        private IWebDriver driver;
        private WebDriverWait wait;
        private MainPage mainPage;

        public Application()
        {
            //driver = new EventFiringWebDriver(new ChromeDriver());
            driver = new ChromeDriver();
            //driver = new EdgeDriver();
            //driver = new FirefoxDriver();
            mainPage = new MainPage(driver);
        }

        internal Application Open(string site)
        {
            mainPage.Open(site);
            return this;
        }

        internal Application AddInCardFirstProduct(int count=1)
        {
            for (int i = 0; i<count; i++)
            {
                mainPage.SelectProduct(0).AddInCart();
            }
            return this;
        }

        internal void RemoveAllProduct()
        {
            mainPage.GoToCart().DeleteAllItem();
        }

        public void Quit()
        {
            driver.Quit();
            driver = null;
        }
    }
}
