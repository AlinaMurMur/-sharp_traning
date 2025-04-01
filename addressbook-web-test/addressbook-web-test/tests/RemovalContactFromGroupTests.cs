﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace WebAddessbookTests
{
    public class RemovalContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovalContactFromGroupTest()
        {
            app.Contacts.Check();
            app.Groups.Check();

            app.Contacts.AddAnyContactToAnyGroup();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList[0];

            app.Contacts.RemovalContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            NUnit.Framework.Assert.AreEqual(oldList, newList);
        }
    }
}
