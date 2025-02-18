using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            app.Groups.Check();

            GroupData newData = new GroupData("zzz");
            newData.Header = "www";
            newData.Footer = "qqq";

            app.Groups.Modify(1, newData);
        }
    }
}
