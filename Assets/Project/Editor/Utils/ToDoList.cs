using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ToDoList
    {
        private ToDoListSearch toDoListSearch;
        private ToDoListGUI toDoListGUI;

        private List<string> scriptNames = new List<string>();
        private List<TaskLine> tasks = new List<TaskLine>();

        public List<string> ScriptNames => scriptNames;

        public List<TaskLine> Tasks { get { return tasks; } set { tasks = value; } }

        public void Init()
        {
            if (toDoListSearch == null)
            {
                toDoListSearch = new ToDoListSearch(this);
            }

            if (toDoListGUI == null)
            {
                toDoListGUI = new ToDoListGUI(this);
            }
        }

        public void Search() => toDoListSearch.Search(ref tasks, ref scriptNames, -1);

        public void SearchByHastag(int hastagID) => toDoListSearch.Search(ref tasks, ref scriptNames, hastagID);

        public void GUI_TaskHeader(CodeTaskLine task) => toDoListGUI.Task_Header(task);

        public void GUI_TagHeader(string header) => toDoListGUI.Task_Tag_Header(header);

        public void GUI_ScriptPath(CodeTaskLine task) => toDoListGUI.Task_ScriptPath(task);

        public void GUI_TaskMessage(CodeTaskLine task) => toDoListGUI.Task_Message(task);

        public void GUI_Button_Done(CodeTaskLine task) => toDoListGUI.Task_DoneButton(task);

        public void GUI_Button_Script(CodeTaskLine task) => toDoListGUI.Task_ScriptButton(task);

        public void GUI_Tag(CodeTaskLine task) => toDoListGUI.Task_Tag(task);

        public void GUI_SetPriorityColor(int taskPriority)
        {
            if (taskPriority == -1)
            {
                GUI.backgroundColor = Color.white;
            }

            GUI.backgroundColor = ToDoListUtils.GetPriorityColor(taskPriority);
        }

        public void GUI_DrawLine(Color color) => ToDoListUtils.DrawUILine(color);

        public void SortTasksByPriority() => ToDoListUtils.SortTasksByPriority(ref tasks);
    }
}