using System;
using Utils;

[Serializable]
public class LogOverridePref
{
    public Category category;
    public Priority logLevel = Priority.Info;
}