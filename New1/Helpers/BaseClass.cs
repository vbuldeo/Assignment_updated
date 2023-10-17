using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using NUnit;
using OpenQA.Selenium.Firefox;

namespace Assignment_Package.Helpers
{
    [SetUpFixture]

    internal class BaseClass
    {
        public IWebDriver _driver;


        [SetUp]
        public void start_Browser()
        {
            // Local Selenium WebDriver

            //ChromeOptions options = new ChromeOptions();
            //FirefoxDriver options = new FirefoxDriver();
            //_driver = new ChromeDriver(options);
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        [TearDown]
        public void close_Browser()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

        public IWebDriver GetDriver()
        {
            return _driver;
        }
    }//Test 
}

