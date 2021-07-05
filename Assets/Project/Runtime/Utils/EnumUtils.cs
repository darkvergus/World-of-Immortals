using System;
using System.Linq;

namespace Utils
{
    public static class EnumUtils
    {
        public static T Max<T>() => Enum.GetValues(typeof(T)).Cast<T>().Max();

        public static T Min<T>() => Enum.GetValues(typeof(T)).Cast<T>().Min();

        public static T Next<T>(this T src) where T : struct => Enum.GetValues(src.GetType()).Cast<T>().Concat(new[] { default(T) }).SkipWhile(e => !src.Equals(e)).Skip(1).First();

        public static T Previous<T>(this T src) where T : struct => Enum.GetValues(src.GetType()).Cast<T>().Concat(new[] { default(T) }).Reverse().SkipWhile(e => !src.Equals(e)).Skip(1).First();

        public static T First<T>() => Enum.GetValues(typeof(T)).Cast<T>().First();
    }
}