using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    public class NavigationHelper(ApplicationManager manager, string baseURL) : HelperBase(manager)
    {
        private string baseURL = baseURL;

        public void GoToHomePage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }

        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "group.php"
                && IsElementPresent(By.Name("new"))) 
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void GoToContactsPage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
