using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }
        public ProjectManagementHelper Create(ProjectData project)
        {
            OpenPageProjectManagement();
            ProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            return this;
        }
        public ProjectManagementHelper OpenPageProjectManagement()
        {
            driver.FindElement(By.LinkText("Управление")).Click();
            driver.FindElement(By.LinkText("Проекты")).Click();
            return this;
        }
        private ProjectManagementHelper ProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            return this;
        }
        private ProjectManagementHelper FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).Click();
            driver.FindElement(By.Id("project-name")).Clear();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            return this;
        }
        private ProjectManagementHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
            return this;
        }

        public void ViewListInProject()
        {
            OpenPageProjectManagement();
            var projectsTable = driver.FindElement(
                By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']"));
            var projectRows = projectsTable.FindElements(By.XPath(".//tbody/tr"));

            if (projectRows.Count == 0)
            {
                ProjectData createdProjectToRemove = new ProjectData("Добавляем проект");
                Create(createdProjectToRemove);
            }
        }
        public List<ProjectData> GetAllProjecstList()
        {
            if (projectCash == null)
            {
                projectCash = new List<ProjectData>();

               // WaitWhileProjectTableAppear();

                ICollection<IWebElement> rows = driver.FindElements(
                    By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr")
                );
                foreach (IWebElement row in rows)
                {
                    try
                    {
                        string id = row.FindElement(By.XPath("./td/a")).GetAttribute("href").Split('=')[1];
                        string name = row.FindElement(By.XPath("./td/a")).Text;
                        string description = row.FindElement(By.XPath("./td[5]")).Text;

                        ProjectData project = new ProjectData(name)
                        {
                            Id = id
                        };

                        projectCash.Add(project);
                    }
                    catch (NoSuchElementException ex)
                    {
                        Console.WriteLine($"Ошибка при извлечении данных: {ex.Message}");
                    }
                }
            }
            return new List<ProjectData>(projectCash);
        }

        public ProjectData TakeProject()
        {
            //WaitWhileProjectTableAppear();
            IWebElement firstRow = driver.FindElement(
                By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr[1]")
            );
            try
            {
                string id = firstRow.FindElement(By.XPath("./td/a")).GetAttribute("href").Split('=')[1];
                string name = firstRow.FindElement(By.XPath("./td/a")).Text;
                string description = firstRow.FindElement(By.XPath("./td[5]")).Text;

                ProjectData takedFirstProject = new ProjectData(name)
                {
                    Description = description,
                    Id = id
                };
                return takedFirstProject;
            }
            catch (NoSuchElementException ex)
            {
                return null;
            }
        }
        public void RemoveProject(int index)
        {
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div/table/tbody/tr[" + index + "]/td/a")).Click();
            driver.FindElement(By.XPath("//form[@id='manage-proj-update-form']/div/div[3]/button[2]")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }
        public List<ProjectData> GetProjectList()
        {
            if (projectCash == null)
            {
                projectCash = new List<ProjectData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr"));
                foreach (IWebElement element in elements)
                {
                    String collectName = element.FindElement(By.XPath("td[1]/a")).Text;

                    projectCash.Add(new ProjectData(collectName));
                }
            }
            return new List<ProjectData>(projectCash);
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr")).Count;
        }

        private List<ProjectData> projectCash = null;
        //private void WaitWhileProjectTableAppear()
       // {
           // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
          //  wait.Until(d => d.FindElement(By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']")));
        //}
        public async Task<List<ProjectData>> GetProjectListAPI()
        {
            var projectList = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            var projects = await client.mc_projects_get_user_accessibleAsync("administrator", "root");
            foreach (var project in projects)
            {
                string projectName = project.name;

                 projectList.Add(new ProjectData(projectName));
            }
            return new List<ProjectData>(projectList);
        }
    }
}