﻿using System;
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
            ContactData newData = new ContactData("LastName", "Name");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(1, newData);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            NUnit.Framework.Assert.AreEqual(oldContacts, newContacts);
        }
    }
}