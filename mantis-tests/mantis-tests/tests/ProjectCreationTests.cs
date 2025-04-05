using System;
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
            ProjectData createdProject = new ProjectData(GenerateRandomString(9));

            List<ProjectData> beforeCreatejectsList = app.Projects.GetAllProjecstList();

            app.Projects.Create(createdProject);

            app.Projects.OpenPageProjectManagement();

            List<ProjectData> afterCreateProjectsList = app.Projects.GetAllProjecstList();
            beforeCreatejectsList.Add(createdProject);

            beforeCreatejectsList.Sort();
            afterCreateProjectsList.Sort();
            NUnit.Framework.Assert.AreEqual(beforeCreatejectsList, afterCreateProjectsList);
            // ProjectData Project = new ProjectData(GenerateRandomString(9));

            //app.API.GetProjectsByApi();

            // app.Projects.Create(Project);
        }

        [Test]
        public async Task ProjectCreationTestAPI()
        {
            ProjectData project = new ProjectData(GenerateRandomString(9));

            List<ProjectData> oldProjects = await app.Projects.GetProjectListAPI();

            app.Projects.Create(project);

            NUnit.Framework.Assert.AreEqual(oldProjects.Count + 1, app.Projects.GetProjectCount());

            List<ProjectData> newProjects = await app.Projects.GetProjectListAPI();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            NUnit.Framework.Assert.AreEqual(oldProjects, newProjects);
        }
    }
}