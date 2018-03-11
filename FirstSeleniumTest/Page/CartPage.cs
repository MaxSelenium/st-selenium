using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSeleniumTest.Page
{
    internal class CartPage:PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "li.shortcut")]
        IWebElement buttonAdd;
        [FindsBy(How = How.XPath, Using = "//button[@name='remove_cart_item']")]
        IWebElement butRemove;
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        internal CartPage DeleteAllItem()
        {
            while (!Tools.IsExistsPresent(driver,By.XPath("//a[.='<< Back']")))
            {
                if (Tools.IsExistsPresent(driver, By.CssSelector("li.shortcut")))
                {
                    buttonAdd.Click();
                }
                var elem = driver.FindElement(By.XPath("//table[@class='dataTable rounded-corners']"));
                butRemove.Click();
                wait.Until(ExpectedConditions.StalenessOf(elem));
            }
            return this;
        }
    }
}
