using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using LinqToDB.Mapping;

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

            List<GroupContactRelation> gcrs = GroupContactRelation.GetAll();
            string groupId, contactId;
            GroupContactRelation gcr = gcrs[0];
            groupId = gcr.GroupId;
            contactId = gcr.ContactId;
            GroupData group = GroupData.GetAll().FirstOrDefault(g => g.Id == groupId);

            List<ContactData> oldList = group.GetContacts();

            app.Contacts.SelectGroupToRemoval(group.Name);
            app.Contacts.SelectContactToRemoval(contactId);
            app.Contacts.CommitRemovalContactFromGroup();

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            NUnit.Framework.Assert.AreEqual(oldList, newList);
        }
    }
}
