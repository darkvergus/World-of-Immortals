using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class ToDoListEditor : EditorWindow
    {
        private ToDoList toDoList;

        private Vector2 scrollPosition;
        private bool settings;

        private string selectedTag;
        private string selectedHash;

        private enum SortFilterType { NONE, PRIORITY, TAG, HASHTAG }

        private enum GuiType { SIMPLE, GROUP }

        private GuiType guiMode = GuiType.GROUP;
        private SortFilterType filterType = SortFilterType.NONE;

        [MenuItem("Todo/ToDoList", false, 1)]
        private static void WindowInit()
        {
            ToDoListEditor window = (ToDoListEditor)GetWindow(typeof(ToDoListEditor), false, "To Do List");
            window.Show();
        }

        private void Init()
        {
            if (toDoList == null)
            {
                toDoList = new ToDoList();
            }
        }

        private void OnGUI()
        {
            Init();
            toDoList.Init();

            if (filterType == SortFilterType.HASHTAG)
            {
                toDoList.SearchByHastag(ToDoListUtils.ActiveHastags.ToList().IndexOf(selectedHash));
            }
            else
            {
                toDoList.Search();
            }

            GUIRenderer();
        }

        private void GUIRenderer()
        {
            settings = EditorGUILayout.Foldout(settings, "Settings:");
            if (settings)
            {
                SettingsLayout();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            if (filterType == SortFilterType.TAG)
            {
                GroupTagsLayout(toDoList.Tasks, selectedTag);
            }
            else
            {
                if (guiMode == GuiType.GROUP)
                {
                    GroupTaskLayout(toDoList.Tasks);
                }
                else if (guiMode == GuiType.SIMPLE)
                {
                    if (filterType == SortFilterType.PRIORITY)
                    {
                        toDoList.SortTasksByPriority();
                    }

                    foreach (var task in toDoList.Tasks)
                    {
                        CodeTaskLine codeTaskLine = (CodeTaskLine)task;
                        SimpleTaskLayout(codeTaskLine);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void GroupTagsLayout(List<TaskLine> tasks, string tag)
        {
            List<CodeTaskLine> filteredTasks = new List<CodeTaskLine>();
            foreach (TaskLine task in tasks)
            {
                CodeTaskLine codeTaskLine = (CodeTaskLine)task;
                if (codeTaskLine.Tags.Contains(tag))
                {
                    filteredTasks.Add(codeTaskLine);
                }
            }

            EditorGUILayout.BeginVertical("Box");
            toDoList.GUI_TagHeader(tag);

            foreach (CodeTaskLine task in filteredTasks)
            {
                toDoList.GUI_SetPriorityColor(task.TaskPriority);
                EditorGUILayout.BeginVertical("Box");

                EditorGUILayout.BeginHorizontal();
                toDoList.GUI_TaskMessage(task);
                toDoList.GUI_Button_Done(task);
                toDoList.GUI_Button_Script(task);
                EditorGUILayout.EndHorizontal();

                toDoList.GUI_SetPriorityColor(-1);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        private void GroupTaskLayout(List<TaskLine> tasks)
        {
            Group group = new Group();
            foreach (TaskLine task in tasks)
            {
                CodeTaskLine codeTaskLine = (CodeTaskLine)task;
                group.AddNew(codeTaskLine);
            }

            if (filterType == SortFilterType.PRIORITY)
            {
                group.GroupTasks = group.SortDicByValue();
                foreach (GroupTask task in group.GroupTasks.Values)
                {
                    ToDoListUtils.SimpleTaskSort(task.Tasks);
                }
            }

            int taskID = 0;
            foreach (string key in group.GroupTasks.Keys)
            {
                EditorGUILayout.BeginVertical("Box");
                GroupTask groupTask = group.GroupTasks[key];
                toDoList.GUI_TaskHeader(groupTask.Tasks[taskID]);

                for (int i = 0; i < group.GroupTasks[key].Tasks.Count; i++)
                {
                    toDoList.GUI_SetPriorityColor(groupTask.Tasks[taskID].TaskPriority);
                    EditorGUILayout.BeginVertical("Box");

                    if (groupTask.Tasks[taskID].HasTags())
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("");
                        toDoList.GUI_Tag(groupTask.Tasks[taskID]);
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    toDoList.GUI_TaskMessage(groupTask.Tasks[taskID]);
                    toDoList.GUI_Button_Done(groupTask.Tasks[taskID]);
                    toDoList.GUI_Button_Script(groupTask.Tasks[taskID]);
                    EditorGUILayout.EndHorizontal();

                    toDoList.GUI_SetPriorityColor(-1);
                    EditorGUILayout.EndHorizontal();
                    taskID++;
                }

                taskID = 0;
                EditorGUILayout.EndVertical();
            }
        }

        private void SimpleTaskLayout(CodeTaskLine codeTaskLine)
        {
            toDoList.GUI_SetPriorityColor(codeTaskLine.TaskPriority);
            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();
            toDoList.GUI_TaskHeader(codeTaskLine);
            toDoList.GUI_Tag(codeTaskLine);
            toDoList.GUI_Button_Done(codeTaskLine);
            toDoList.GUI_Button_Script(codeTaskLine);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2);

            toDoList.GUI_DrawLine(Color.gray);
            EditorGUILayout.BeginHorizontal();
            toDoList.GUI_TaskMessage(codeTaskLine);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            toDoList.GUI_SetPriorityColor(-1);
        }

        private void SettingsLayout()
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            guiMode = (GuiType)EditorGUILayout.EnumPopup("Show Mode: ", guiMode);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            filterType = (SortFilterType)EditorGUILayout.EnumPopup("Sorting by: ", filterType);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUI.indentLevel++;
            if (filterType == SortFilterType.HASHTAG)
            {
                selectedHash = ToDoListUtils.DropDownSelector("Select Hash: ", selectedHash, ToDoListUtils.ActiveHastags);
            }

            if (filterType == SortFilterType.TAG)
            {
                selectedTag = ToDoListUtils.DropDownSelector("Select Tag: ", selectedTag, ToDoListUtils.ActiveTags.ToArray());
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}