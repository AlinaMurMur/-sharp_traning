﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Mantis;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData("administrator", "root");
            IssueData issue = new IssueData()
            {
                Summary = "some short text",
                Description = "some long text",
                Category = "General"
            };

            ProjectDataId projectId = new ProjectDataId()
            {
                Id = "1"
            };

            app.API.CreateNewIssue(account, projectId, issue);
        }
    }
}
