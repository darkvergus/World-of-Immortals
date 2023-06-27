namespace Utils
{
    public static class RichTextUtils
    {
        public static string Bold(this object obj) => $"<b>{obj}</b>";
        public static string Italic(this object obj) => $"<i>{obj}</i>";
        public static string SetSize(this object obj, int size) => $"<size={size}>{obj}</size>";
        public static string Aqua(this object obj) => ColorProcessor(obj, "aqua");
        public static string Black(this object obj) => ColorProcessor(obj, "black");
        public static string Blue(this object obj) => ColorProcessor(obj, "blue");
        public static string Brown(this object obj) => ColorProcessor(obj, "brown");
        public static string Cyan(this object obj) => ColorProcessor(obj, "cyan");
        public static string DarkBlue(this object obj) => ColorProcessor(obj, "darkblue");
        public static string Fuchsia(this object obj) => ColorProcessor(obj, "fuchsia");
        public static string Green(this object obj) => ColorProcessor(obj, "green");
        public static string Grey(this object obj) => ColorProcessor(obj, "grey");
        public static string LightBlue(this object obj) => ColorProcessor(obj, "lightblue");
        public static string Lime(this object obj) => ColorProcessor(obj, "lime");
        public static string Magenta(this object obj) => ColorProcessor(obj, "magenta");
        public static string Maroon(this object obj) => ColorProcessor(obj, "maroon");
        public static string Navy(this object obj) => ColorProcessor(obj, "navy");
        public static string Olive(this object obj) => ColorProcessor(obj, "olive");
        public static string Orange(this object obj) => ColorProcessor(obj, "orange");
        public static string Purple(this object obj) => ColorProcessor(obj, "purple");
        public static string Red(this object obj) => ColorProcessor(obj, "red");
        public static string Silver(this object obj) => ColorProcessor(obj, "silver");
        public static string Teal(this object obj) => ColorProcessor(obj, "teal");
        public static string White(this object obj) => ColorProcessor(obj, "white");
        public static string Yellow(this object obj) => ColorProcessor(obj, "yellow");

        private static string ColorProcessor(object obj, string color) => $"<color={color}>{obj}</color>";
    }

}
