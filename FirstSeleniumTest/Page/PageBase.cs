using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FirstSeleniumTest.Page
{
    internal class PageBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
    }
}
