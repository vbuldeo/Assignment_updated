using Assignment_Package.Helpers;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using NUnit;
using System.Security.Cryptography;



namespace Assignment_Package
{
    [TestFixture]

    class Assignment_Test : BaseClass

    {

        [Test]

        public void Test1()
        {
            IWebDriver driver = GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Actions a = new Actions(driver);


            String moneycorp_url = "https://www.moneycorp.com/en-gb/";
            String comparing_url = "https://www.moneycorp.com/en-us/";
            String search_keyword = "international payments";
            String search_paramter = search_keyword.Replace(' ', '+');
            IList<IWebElement> searcharticle_links = driver.FindElements(By.XPath("//a[@class='title u-m-b2']"));


            //1. Navigate to MoneyCorp URL and Verify
            driver.Navigate().GoToUrl(moneycorp_url);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='c-header__logo']")));
            StringAssert.Contains("en-gb", driver.Url);
            StringAssert.Contains("Moneycorp", driver.FindElement(By.XPath("//div[@class='c-header__logo']/a")).GetAttribute("title"));
            Console.WriteLine("MoneyCorp Url has been verified");


            //2. Change Lanaguage to USA English and validate
            if (driver.FindElement(By.XPath("//button[@id='language-dropdown-flag']/span/img")).GetAttribute("src") != "america")
            {
                driver.FindElement(By.XPath("//button[@id='language-dropdown-flag']")).Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li/descendant::img[contains(@src, 'america')]")));
                driver.FindElement(By.XPath("//li/descendant::img[contains(@src, 'america')]")).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='language-dropdown-flag']/span/img[contains(@src, 'america')]")));
                StringAssert.Contains("en-us", driver.Url);
                Console.WriteLine("Language is changed to USA America");
            } else
            {
                if (driver.FindElement(By.XPath("//button[@id='language-dropdown-flag']/span/img")).GetAttribute("src") == "america")
                {
                    StringAssert.Contains("en-us", driver.Url);
                    Console.WriteLine("Selected Language is USA America");
                }
            }


            //3. Click 'Find out more' for “Foreign exchange solutions” article and validate if user is on the correct page
            a.MoveToElement(driver.FindElement(By.XPath("//article/div/a[@href='/en-us/business/foreign-exchange-solutions/']")));
            driver.FindElement(By.XPath("//article/div/a[@href='/en-us/business/foreign-exchange-solutions/']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//article/h1[@class='u-text-display u-text-h1@l']")));

            //Validate if User is on the Foreign exchange solutions Page
            StringAssert.Contains("Foreign exchange solutions", driver.FindElement(By.XPath("//article/h1[@class='u-text-display u-text-h1@l']")).Text);
            StringAssert.Contains("foreign-exchange-solutions", driver.Url);
            Console.WriteLine("User is navigated to Foriegn Exchange Solutions Page");


            //4. Search for "international payments” using the search box
            a.MoveToElement(driver.FindElement(By.XPath("//div[@class='c-header__wrap']/descendant::form[2]/input[@id='nav_search']")));
            driver.FindElement(By.XPath("//div[@class='c-header__wrap']/descendant::form[2]/input[@id='nav_search']")).Clear();
            driver.FindElement(By.XPath("//div[@class='c-header__wrap']/descendant::form[2]/input[@id='nav_search']")).SendKeys(search_keyword);
            driver.FindElement(By.XPath("//div[@class='c-header__wrap']/descendant::form[2]/input[@type='submit']")).Click();
            Console.WriteLine("User is able to search " + search_keyword + " using search box");


            //5. Validate if user is navigated to Search Results Page
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//section/div[contains(@class, 'header search')]")));
            StringAssert.Contains("search", driver.Url);
            StringAssert.Contains(search_keyword , driver.FindElement(By.XPath("//form[contains(@action , 'search')]/input[@class='chosen-select']")).GetAttribute("value"));
            StringAssert.Contains(search_paramter, driver.Url);
            Console.WriteLine("User is on the Search Results Page and able to see the searched keyword on the page");


            //6. Validate if each article in the search list displays a link that starts with 
            int article_count = searcharticle_links.Count;

            for (int i = 0; i < article_count; i++)
            {
                StringAssert.StartsWith(comparing_url, searcharticle_links[i].GetAttribute("href"));
            }
            Console.WriteLine("All the article URL starts with " + comparing_url);

        }

    }
}





  

