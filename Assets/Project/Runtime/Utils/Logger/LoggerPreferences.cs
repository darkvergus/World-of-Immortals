using System;
using System.Collections.Generic;

[Serializable]
public class LoggerPreferences
{
	public List<LogOverridePref> logOverrides = new();
}