using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace FirstSeleniumTest.Page
{
    internal class ProductPage : PageBase
    {


        [FindsBy(How = How.XPath, Using = "//button[@name='add_cart_product']")]
        IWebElement buttonAdd;

        [FindsBy(How = How.CssSelector, Using = "div#logotype-wrapper")]
        IWebElement logo;

        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal ProductPage AddInCart(string size = "Small")
        {
            if (Tools.IsExistsPresent(driver, By.ClassName("options")))
            {
                SelectSize(size);
            }
            var element = driver.FindElement(By.CssSelector("span.quantity"));
            int q = Convert.ToInt32(element.Text);
            buttonAdd.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, (q + 1).ToString()));
            return this;
        }

        internal ProductPage SelectSize(string val)
        {
            var select = new SelectElement(driver.FindElement(By.XPath("//select[contains(@name,'Size')]")));
            select.SelectByValue(val);
            return this;
        }

        internal MainPage BackToMain()
        {
            logo.Click();
            return new MainPage(driver);
        }
    }
}
