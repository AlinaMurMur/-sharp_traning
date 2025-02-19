using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            app.Groups.Remove(1);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            NUnit.Framework.Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);
        }
    }
}
