using FirstSeleniumTest.Manager;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstSeleniumTest.Test
{
    public class TestBase
    {
        private static ThreadLocal<Application> tlApp = new ThreadLocal<Application>();
        public Application app;

        [SetUp]
        public void Start()
        {
            if (tlApp.Value != null)
            {
                app = tlApp.Value;
                return;
            }
            app = new Application();
            tlApp.Value = app;
        }

        [TearDown]
        public void Stop()
        {
            tlApp.Dispose();
            app.Quit();
        }
    }
}
