using System;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public struct TextureIcons : IEquatable<TextureIcons>
    {
        [SerializeField]
        private Texture2D iconScript;

        [SerializeField]
        private Texture2D iconDone;
        
        public Texture2D[] iconFlags;

        public Texture2D IconScript { get { return iconScript; }    set { iconScript = value; } }
        public Texture2D IconDone { get { return iconDone; } set { iconDone = value; } }

        public bool Equals(TextureIcons other) => other is TextureIcons otherS && Equals(otherS);
    }

    public class ToDoListGUI
    {
        private ToDoList toDoList;
        private ToDoListConfig toDoListConfig;

        private TextureIcons icons;
        private const float ICON_SIZE = 20f;

        public ToDoListGUI(ToDoList toDoList)
        {
            this.toDoList = toDoList;
            Init();
        }

        public void Init()
        {
            toDoListConfig = ScriptableObject.CreateInstance<ToDoListConfig>();
            InitTextures();
        }

        private void InitTextures()
        {
            icons.IconDone = toDoListConfig.Icon_Done;
            icons.IconScript = toDoListConfig.Icon_Script;
        }

        public GUIContent GetPriorityFlag(int priority) => CreateGUIIcon(icons.iconFlags[priority]);

        private GUIContent CreateGUIIcon(Texture2D icon) => new GUIContent(icon);

        public void Task_DoneButton(CodeTaskLine task)
        {
            GUI.backgroundColor = Color.white;

            if (GUILayout.Button(CreateGUIIcon(icons.IconDone), GUILayout.Width(20), GUILayout.Height(20)))
            {
                ToDoListUtils.RemoveLineFromScript(task.ScriptPath, task);
            }

            GUI.backgroundColor = ToDoListUtils.GetPriorityColor(task.TaskPriority);
        }

        public void Task_ScriptButton(CodeTaskLine task)
        {
            GUI.backgroundColor = Color.white;

            if (GUILayout.Button(CreateGUIIcon(icons.IconScript), GUILayout.Width(ICON_SIZE), GUILayout.Height(ICON_SIZE)))
            {
                AssetDatabase.OpenAsset(task.Script, task.Line);
            }

            GUI.backgroundColor = ToDoListUtils.GetPriorityColor(task.TaskPriority);
        }

        public void Task_Tag(CodeTaskLine task)
        {
            for (int i = 0; i < task.Tags.Count; i++)
            {
                GUI.backgroundColor = Color.white;
                GUILayout.Button(task.Tags[i], "MiniButton", GUILayout.Width(10 * task.Tags[i].Length));
                GUI.backgroundColor = ToDoListUtils.GetPriorityColor(task.TaskPriority);
            }
        }

        public void Task_Header(CodeTaskLine task) => EditorGUILayout.LabelField($"{task.ScriptName.Replace(task.ScriptName.Substring(0, 1), task.ScriptName.Substring(0, 1).ToUpper())}.{(task.ScriptPath.EndsWith(".cs") ? "cs" : "cs")}", EditorStyles.boldLabel);

        public void Task_Tag_Header(string header) => GUILayout.Button(header, "MiniButton", GUILayout.Width(10 * header.Length));

        public void Task_ScriptPath(CodeTaskLine task) => EditorGUILayout.LabelField(string.Format("{0} at line {1}.", task.ScriptPath, task.Line));

        public void Task_Message(CodeTaskLine task)
        {
            EditorGUILayout.LabelField(string.Format("{0}:", task.Hastag), EditorStyles.boldLabel, GUILayout.Width(60));
            EditorGUILayout.LabelField(string.Format("({0}) {1}", task.Line, task.Message, EditorStyles.wordWrappedLabel));
        }
    }
}