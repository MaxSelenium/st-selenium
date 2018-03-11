using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSeleniumTest.Page
{
    internal class MainPage: PageBase
    {
        [FindsBy(How = How.CssSelector, Using = ".product")]
        IList<IWebElement> products;
        [FindsBy(How = How.XPath, Using = "//a[.='Checkout »']")]
        IWebElement checkout;

        //driver.FindElements(By.XPath("//li[@class='product column shadow hover-light']"))[0].Click();
        public MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        internal MainPage Open(string site)
        {
            driver.Url = site;
            return this;
        }
        internal ProductPage SelectProduct(int i=0)
        {
            products[i].Click();
            return new ProductPage(driver);
        }

        internal CartPage GoToCart()
        {
            checkout.Click();
            return new CartPage(driver);
        }
    }
}
