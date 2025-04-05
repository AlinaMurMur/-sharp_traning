using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            app.Projects.ViewListInProject();

            List<ProjectData> beforeDeletionjectsList = app.Projects.GetProjectList();

            ProjectData projectToDelete = app.Projects.TakeProject();
            app.Projects.RemoveProject(1);

            List<ProjectData> afterCreateProjectsList = app.Projects.GetProjectList();
            beforeDeletionjectsList.Remove(projectToDelete);
            beforeDeletionjectsList.Sort();
            afterCreateProjectsList.Sort();
            NUnit.Framework.Assert.AreEqual(beforeDeletionjectsList, afterCreateProjectsList);
        }

        [Test]
        public async Task ProjectRemovalTestAPI()
        {
            app.Projects.ViewListInProject();

            List<ProjectData> beforeDeletionjectsList = await app.Projects.GetProjectListAPI();

            ProjectData projectToDelete = app.Projects.TakeProject();
            app.Projects.RemoveProject(1);

            List<ProjectData> afterCreateProjectsList = await app.Projects.GetProjectListAPI();
            beforeDeletionjectsList.Remove(projectToDelete);
            beforeDeletionjectsList.Sort();
            afterCreateProjectsList.Sort();
            NUnit.Framework.Assert.AreEqual(beforeDeletionjectsList, afterCreateProjectsList);
        }
    }
}