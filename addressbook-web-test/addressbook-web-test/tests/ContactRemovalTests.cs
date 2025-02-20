using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using WebAddessbookTests;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.Check();

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.Remove();

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            //NUnit.Framework.Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);
            oldContacts.RemoveAt(0);
            NUnit.Framework.Assert.AreEqual(oldContacts, newContacts);
        }
    }
}