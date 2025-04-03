﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {

        [Test]
        public void ProjectCreationTest()
        {
            ProjectData Project = new ProjectData(GenerateRandomString(9));

            app.API.GetProjectsByApi();

            app.Projects.Create(Project);
        }
    }
}