using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSeleniumTest
{
    public static class Tools
    {
        public static bool IsExists(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        public static bool IsExists(IWebDriver driver, IWebElement elem, By locator)
        {
            return elem.FindElements(locator).Count > 0;
        }

        public static bool IsExistsPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                return driver.FindElements(locator).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
        }
        public static bool IsExistsPresent(IWebDriver driver, IWebElement elem, By locator)
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
    }
}
