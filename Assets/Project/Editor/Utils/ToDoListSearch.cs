using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Utils
{
    public class ToDoListSearch
    {
        private ToDoList toDoList;
        private string[] HASTAGS_PREFIX = new[] { "TODO", "FIX", "BUG", "IMPROVE" };

        public ToDoListSearch(ToDoList toDoList) => this.toDoList = toDoList;

        public void Search(ref List<TaskLine> tags, ref List<string> scriptNames, int hastagID = -1)
        {
            tags = new List<TaskLine>();
            scriptNames = new List<string>();

            string[] assetPaths = AssetDatabase.GetAllAssetPaths();

            List<MonoScript> scripts = new List<MonoScript>();
            Dictionary<string, string> pathScript = new Dictionary<string, string>();

            HandleCorrectScripts(assetPaths, ref scripts, ref pathScript);

            if (hastagID != -1)
            {
                FindHastagInScripts(scripts, pathScript, HASTAGS_PREFIX[hastagID]);
                return;
            }

            for (int i = 0; i < HASTAGS_PREFIX.Length; i++)
            {
                FindHastagInScripts(scripts, pathScript, HASTAGS_PREFIX[i]);
            }
        }

        private void HandleCorrectScripts(string[] assetPaths, ref List<MonoScript> scripts, ref Dictionary<string, string> pathScript)
        {
            foreach (string assetPath in assetPaths)
            {
                if (assetPath.Contains("/Editor/") || !assetPath.Contains("Assets")) 
                    continue;
                if (assetPath.EndsWith(".cs"))
                {
                    MonoScript mono = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
                    scripts.Add(mono);
                    if (!pathScript.ContainsKey(mono.name))
                        pathScript.Add(mono.name, assetPath);
                }
            }
        }

        private void FindHastagInScripts(List<MonoScript> scripts, Dictionary<string, string> pathScript, string hashTag)
        {
            foreach (MonoScript script in scripts)
            {
                string scriptText = script.text;
                int lineCount = 0;

                while (scriptText != string.Empty)
                {
                    if (scriptText.Contains(hashTag))
                    {
                        int taskPriority = PriorityHandler(scriptText, hashTag);
                        int start = scriptText.IndexOf(hashTag, StringComparison.Ordinal) + hashTag.Length;
                        int end = scriptText.Substring(start).IndexOf("\n", StringComparison.Ordinal);

                        for (int i = 0; i < start + end + 1; i++)
                        {
                            if (scriptText[i] == '\n')
                                lineCount++;
                        }

                        string todo = scriptText.Substring(start, end).Replace(":", "");
                        scriptText = scriptText.Substring(start + end + 1);

                        List<string> tags = TagHandler(ref todo);

                        if (!toDoList.ScriptNames.Contains(script.name)) 
                            toDoList.ScriptNames.Add(script.name);
                        AddNewCodeTask(toDoList.Tasks, script, todo, script.name, lineCount, pathScript[script.name], taskPriority, hashTag, tags);
                    }
                    else
                    {
                        lineCount = 0;
                        scriptText = string.Empty;
                    }
                }
            }
        }

        private int PriorityHandler(string scriptText, string hashTag)
        {
            int priorityTask = 0;
            string prioritySubString = scriptText.Substring(scriptText.IndexOf(hashTag, StringComparison.Ordinal) - 3, 3);
            for (int i = 0; i < prioritySubString.Length; i++)
            {
                if (prioritySubString[i] == '!') 
                    priorityTask++;
            }

            return priorityTask;
        }

        private List<string> TagHandler(ref string text)
        {
            List<string> tags = new List<string>();
            int startTag = text.IndexOf("<", StringComparison.Ordinal);
            int endTag = text.IndexOf(">", StringComparison.Ordinal);

            if (startTag != -1)
            {
                string tagString = text.Substring(startTag, endTag + 1);
                text = text.Remove(startTag, (endTag - startTag) + 1);
                tagString = tagString.Remove(0, 1);
                tagString = tagString.Remove(tagString.Length - 1, 1);

                if (tagString.Contains(","))
                    tags = tagString.Split(',').ToList();
                else
                    tags.Add(tagString);
            }
            ToDoListUtils.ActiveTags.AddRange(from tag in tags where !ToDoListUtils.ActiveTags.Contains(tag) select tag);

            return tags;
        }

        private void AddNewCodeTask(List<TaskLine> tasks, MonoScript script, string todo, string scriptName, int line, string scriptPath, int taskPriority, string hashTag, List<string> tags)
        {
            CodeTaskLine codeTaskLine = new CodeTaskLine(script, todo, scriptName, line, scriptPath, taskPriority, hashTag, tags);
            foreach (var _ in from task in tasks let codeTask = (CodeTaskLine)task where codeTask.HashKey.Equals(codeTaskLine.HashKey) select new { })
            {
                return;
            }

            if (!tasks.Contains(codeTaskLine))
                tasks.Add(codeTaskLine);
        }
    }
}