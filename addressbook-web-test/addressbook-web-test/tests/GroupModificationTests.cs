using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            app.Groups.Check();

            GroupData newData = new GroupData("zzz");
            newData.Header = "www";
            newData.Footer = "qqq";

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModify = oldGroups[0];

            app.Groups.Modify(toBeModify, newData);


            NUnit.Framework.Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            NUnit.Framework.Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModify.Id)
                {
                    NUnit.Framework.Assert.AreEqual(newData.Name, group.Name);
                }
            }

        }
    }
}
