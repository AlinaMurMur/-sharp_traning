using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.Check();

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Remove(0);


            NUnit.Framework.Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            NUnit.Framework.Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                NUnit.Framework.Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
