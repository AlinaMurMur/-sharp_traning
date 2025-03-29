using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPWINTITLE = "Delete group";
        public GroupHelper(ApplicationManager manager) : base(manager) { }
        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.1114f813");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }
        private void CloseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.1114f814");
        }

        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.1114f8112");
            aux.WinWait(GROUPWINTITLE);
        }
        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.1114f811",
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "",
                    "WindowsForms10.SysTreeView32.app.0.1114f811", "GetText", "#0|#" + i, "");
                list.Add(new GroupData()
                {
                    Name = item,
                });
            }
            CloseGroupsDialogue();
            return list;
        }

        public void RemovalGroup()
        {
            OpenGroupsDialogue();
            aux.Send("{DOWN}");
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.1114f811");
            aux.WinWait(DELETEGROUPWINTITLE);
            aux.WinActivate(DELETEGROUPWINTITLE);
            aux.WinWaitActive(DELETEGROUPWINTITLE);
            aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.ControlClick(DELETEGROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            CloseGroupsDialogue();
        }
    }
}
