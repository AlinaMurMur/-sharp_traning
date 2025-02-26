using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebAddessbookTests;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Check()
        {
            CheckContacts();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove()
        {
            SelectContact();
            RemoveContact();

            return this;
        }
        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
            contactCash = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }
        public ContactHelper CheckContacts()
        {
            if (OpenContactPage())
            {
                ContactData contact = new ContactData("Имя", "Фамилия");
                Create(contact);
            }
            return this;
        }

        public bool OpenContactPage()
        {
            return !IsElementPresent(By.Name("selected[]"));
        }

        private ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.FindElement(By.LinkText("home")).Click();
            contactCash = null;
            return this;
        }
        private ContactHelper InitContactModification(int index)
        {
            if (OpenContactPage())
            {
                ContactData contact = new ContactData("Фамилия", "Имя");
                Create(contact);
            }
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public void InitContactDetailsPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
        }
        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCash = null;
            return this;
        }

        private List<ContactData> contactCash = null;
        public List<ContactData> GetContactsList()
        {
            if (contactCash == null)
            {
                contactCash = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id=\"maintable\"]/tbody/tr[@name=\"entry\"]"));
                foreach (IWebElement element in elements)
                {
                    String collectLastname = element.FindElement(By.XPath("td[2]")).Text;
                    String collectFirstname = element.FindElement(By.XPath("td[3]")).Text;

                    contactCash.Add(new ContactData(collectLastname, collectFirstname));
                }
            }
            return new List<ContactData>(contactCash);
        }

        public int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//*[@id=\"maintable\"]/tbody/tr[@name=\"entry\"]")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetailsPage(0);

            string allContactInfo = driver.FindElement(By.CssSelector("div#content")).Text;
            if (allContactInfo == null || allContactInfo == "")
            {
                return "";
            }
            else
            {
                return allContactInfo.
                    Replace("\r\n", " ").
                    Replace("H: ", "").
                    Replace("M: ", "").
                    Replace("W: ", "");
            }
        }
        public string GetContactInformationToEditDetails(int index)
        {
            String lastName = GetContactInformationFromTable(index).Lastname;
            String firstName = GetContactInformationFromTable(index).Firstname;
            String address = GetContactInformationFromTable(index).Address;

            String phones = GetContactInformationFromTable(index).AllPhones;
            String emails = GetContactInformationFromTable(index).AllEmails;

            string allContactInfoTable = (lastName + " " + firstName + " " + address + " " + phones + "  " + emails);

            return allContactInfoTable.Replace("\r\n", " ");
        }
    }
}