using System.Collections.Generic;
using UnityEditor;

namespace Utils
{
    public class CodeTaskLine : TaskLine
    {
        public string Message { get; set; }
        public int Line { get; set; }

        public string ScriptName { get; set; }
        public string ScriptPath { get; set; }

        public MonoScript Script { get; set; }

        public int TaskPriority { get; set; }

        public string Hastag { get; set; }
        public string HashKey { get; set; }

        public List<string> Tags { get; set; }

        public CodeTaskLine(MonoScript script, string message, string scriptName, int line, string scriptPath, int taskPriority, string hastag, List<string> tags)
        {
            Script = script;
            Message = message;
            Line = line;
            ScriptName = scriptName;
            ScriptPath = scriptPath;
            TaskPriority = taskPriority;
            Hastag = hastag;
            Tags = tags;

            HashKey = scriptName + line + message;
        }

        public bool HasTags() => Tags.Count > 0;
    }
}