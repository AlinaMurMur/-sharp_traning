using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using WebAddessbookTests;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.Check();
            ContactData newData = new ContactData("Name", "LastName");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.Modify(1, newData);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            NUnit.Framework.Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }
    }
}