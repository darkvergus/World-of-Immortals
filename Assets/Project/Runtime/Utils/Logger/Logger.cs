#define UNITY_DIALOGS // Comment out to disable dialogs for fatal errors
using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

#if UNITY_EDITOR && UNITY_DIALOGS
#endif

namespace Utils
{
	public static class Logger 
	{
		public static Action LevelChange;
    
    	private static LoggerPreferences loggerPrefs;
    	/// Default Log level
    	public static readonly Priority Priority = Priority.Info;
    	private static Dictionary<Category, Priority> logOverrides = new();
    
    	public static void RefreshPreferences()
    	{
    		string path = Path.Combine(Application.streamingAssetsPath, "LogLevelDefaults/");
    
    		if (!File.Exists(Path.Combine(path, "custom.json")))
    		{
    			string data = File.ReadAllText(Path.Combine(path, "default.json"));
    			File.WriteAllText(Path.Combine(path, "custom.json"), data);
    		}
    
    		loggerPrefs = JsonUtility.FromJson<LoggerPreferences>(File.ReadAllText(Path.Combine(path, "custom.json")));
    
    		logOverrides.Clear();
    
    		foreach (LogOverridePref pref in loggerPrefs.logOverrides)
    		{
    			logOverrides.Add(pref.category, pref.logLevel);
    		}
    	}
    
    	public static void SetLogLevel(Category category, Priority level)
    	{
    		Log($"Log category {category.ToString()} is now set to {level.ToString()}", Category.DebugConsole);
    		int index = loggerPrefs.logOverrides.FindIndex(x => x.category == category);
    		if (index != -1)
    		{
    			loggerPrefs.logOverrides[index].logLevel = level;
    		}
    		else
    		{
    			loggerPrefs.logOverrides.Add(new()
                {
	                category = category, 
	                logLevel = level
                });
    		}
    
    		SaveLogOverrides();
    		RefreshPreferences();
    		LevelChange?.Invoke();
    	}
    
    	public static void SaveLogOverrides()
    	{
    		string path = Path.Combine(Application.streamingAssetsPath, "LogLevelDefaults/");
    		File.WriteAllText(Path.Combine(path, "custom.json"), JsonUtility.ToJson(loggerPrefs));
    	}
    
    	/// <inheritdoc cref="LogTrace"/>
    	/// <inheritdoc cref="LogFormat"/>
    	[StringFormatMethod("msg")]
    	public static void LogTraceFormat(string msg, Category category = Category.Unknown, params object[] args) => TryLog(msg, Priority.Trace, category, args);

        /// LogFormats won't format string if it's not getting printed, therefore perform better.
    	/// This is most useful for Trace level that is rarely enabled.
    	[StringFormatMethod("msg")]
    	public static void LogFormat(string msg, Category category = Category.Unknown, params object[] args) => TryLog(msg, Priority.Info, category, args);

        /// <inheritdoc cref="LogWarning"/>
    	/// <inheritdoc cref="LogFormat"/>
    	[StringFormatMethod("msg")]
    	public static void LogWarningFormat(string msg, Category category = Category.Unknown, params object[] args) => TryLog(msg, Priority.Warning, category, args);

        /// <inheritdoc cref="LogWarning"/>
    	/// <inheritdoc cref="LogFormat"/>
    	[StringFormatMethod("msg")]
    	public static void LogErrorFormat(string msg, Category category = Category.Unknown, params object[] args) => TryLog(msg, Priority.Error, category, args);

        /// Try printing Trace level entry. Most verbose logs that should only be enabled when debugging something that is broken.
    	public static void LogTrace(string msg, Category category = Category.Unknown) => TryLog(msg, Priority.Trace, category);

        /// Try printing Info level entry.
    	public static void Log(string msg, Category category = Category.Unknown) => TryLog(msg, Priority.Info, category);

        /// Try printing Warning level entry.
    	public static void LogWarning(string msg, Category category = Category.Unknown) => TryLog(msg, Priority.Warning, category);

        /// Try printing Error level entry.
    	public static void LogError(string msg, Category category = Category.Unknown) => TryLog(msg, Priority.Error, category);

        private static void TryLog(string message, Priority messageLevel, Category category = Category.Unknown, params object[] args)
    	{
    		if (category == Category.Unknown)
    		{
    			SendLog(message, messageLevel, args);
    			return;
    		}
    
    		Priority referenceLevel = Priority;
    		if (logOverrides.ContainsKey(category))
    		{
    			referenceLevel = logOverrides[category];
    		}
    
    		if (referenceLevel < messageLevel)
    		{
    			return;
    		}
    
    		string categoryPrefix = "[" + category + "] ";
    
    		string msg = categoryPrefix + message;
    		SendLog(message, messageLevel, args);
    	}
    
    	private static void SendLog(string msg, Priority messageLevel, params object[] args)
    	{
    		if (args.Length > 0)
    		{
    			switch (messageLevel)
    			{
    				case Priority.Off:
    					break;
    				case Priority.Error:
    					Debug.LogErrorFormat(msg, args);
    					break;
    				case Priority.Warning:
    					Debug.LogWarningFormat(msg, args);
    					break;
    				case Priority.Info:
    					Debug.LogFormat(msg, args);
    					break;
    				case Priority.Trace:
    					Debug.LogFormat(msg, args);
    					break;
    			}
    		}
    		else
    		{
    			switch (messageLevel)
    			{
    				case Priority.Off:
    					break;
    				case Priority.Error:
    					Debug.LogError(msg);
    					break;
    				case Priority.Warning:
    					Debug.LogWarning(msg);
    					break;
    				case Priority.Info:
    					Debug.Log(msg);
    					break;
    				case Priority.Trace:
    					Debug.Log(msg);
    					break;
    			}
    		}
    	}
    }
}