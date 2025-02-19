using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Имя", "Фамилия");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            NUnit.Framework.Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            NUnit.Framework.Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

        }
    }
}
