using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class ToDoListUtils
    {
        public static readonly string[] ActiveHastags = new[] { "TODO", "FIX", "BUG", "IMPROVE" };
        public static readonly List<string> ActiveTags = new List<string>();

        public static void SortTasksByPriority(ref List<TaskLine> tasks)
        {
            for (int j = 0; j <= tasks.Count - 2; j++)
            {
                for (int i = 0; i <= tasks.Count - 2; i++)
                {
                    CodeTaskLine codeTaskLine = (CodeTaskLine)tasks[i];
                    CodeTaskLine nextCodeTaskLine = (CodeTaskLine)tasks[i + 1];
                    if (codeTaskLine.TaskPriority < nextCodeTaskLine.TaskPriority)
                    {
                        TaskLine temp = tasks[i + 1];
                        tasks[i + 1] = tasks[i];
                        tasks[i] = temp;
                    }
                }
            }
        }

        public static void RemoveLineFromScript(string pathScript, CodeTaskLine codeTaskLine)
        {
            string tempFile = Path.GetTempFileName();

            int l_line = 0;
            using (StreamReader sr = new StreamReader(pathScript))
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                string line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    l_line++;

                    if (l_line != codeTaskLine.Line)
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            File.Delete(pathScript);
            File.Move(tempFile, pathScript);
            AssetDatabase.Refresh();
        }

        public static void DrawUILine(Color color, float thickness = 0.5f, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public static Color GetPriorityColor(int priority)
        {
            return priority switch
            {
                0 => Color.white,
                1 => Color.cyan,
                2 => Color.yellow,
                3 => Color.red,
                _ => Color.white,
            };
        }

        public static string DropDownSelector(string label, string selectedItem, string[] items, params GUILayoutOption[] layoutOptions)
        {
            if (selectedItem == null)
            {
                selectedItem = items[0];
            }

            int oldIndex = Array.IndexOf(items, selectedItem);
            if (oldIndex < 0 || oldIndex > items.Length)
            {
                oldIndex = 0;
                selectedItem = items[0];
            }

            int newIndex = EditorGUILayout.Popup(label, oldIndex, items, layoutOptions);
            selectedItem = items[newIndex];

            return selectedItem;
        }

        public static List<CodeTaskLine> SimpleTaskSort(List<CodeTaskLine> tasks)
        {
            for (int j = 0; j <= tasks.Count - 2; j++)
            {
                for (int i = 0; i <= tasks.Count - 2; i++)
                {
                    CodeTaskLine codeTaskLine = tasks[i];
                    CodeTaskLine nextCodeTaskLine = tasks[i + 1];
                    if (codeTaskLine.TaskPriority < nextCodeTaskLine.TaskPriority)
                    {
                        CodeTaskLine temp = tasks[i + 1];
                        tasks[i + 1] = tasks[i];
                        tasks[i] = temp;
                    }
                }
            }

            return tasks;
        }
    }
}