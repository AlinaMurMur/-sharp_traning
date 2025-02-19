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
            InitContactModification();
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
            return this;
        }
        private ContactHelper InitContactModification()
        {
            if (OpenContactPage())
            {
                ContactData contact = new ContactData("Имя", "Фамилия");
                Create(contact);
            }
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }
        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToContactsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id=\"maintable\"]/tbody/tr[@name=\"entry\"]"));
            foreach (IWebElement element in elements)
            {
                String collectLastname = element.FindElement(By.XPath("td[2]")).Text;
                String collectFirstname = element.FindElement(By.XPath("td[3]")).Text;

                contacts.Add(new ContactData(collectLastname, collectFirstname));
            }

            return contacts;
        }
    }
}