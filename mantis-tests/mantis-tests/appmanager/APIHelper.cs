using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Mantis;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectDataId project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_addAsync(account.Name, account.Password, issue);
        }
        public void GetProjectsByApi()
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
             client.mc_projects_get_user_accessibleAsync("administrator", "root");

        }
        public void CreateProjectsByApi()
       {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = GeneratedRandAzNub(5);
            project.description = GeneratedRandAzNub(8);

            client.mc_project_addAsync("administrator", "root", project);
        }
    }
}
