using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]

        public void TestContactInformation()
        {
           ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            NUnit.Framework.Assert.AreEqual(fromTable, fromForm);
            NUnit.Framework.Assert.AreEqual(fromTable.Address, fromForm.Address);
            NUnit.Framework.Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactDetail()
        {
            string ContactInfoDetail = app.Contacts.GetContactInformationFromDetails(0);
            string ContactInfoTable = app.Contacts.GetContactInformationToEditDetails(0);

            NUnit.Framework.Assert.AreEqual(ContactInfoTable, ContactInfoDetail);

        }
    }
}
