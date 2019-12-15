namespace Software.Main
{
    public static class Marking
    {
        public static string Userid { get; set; }

        public static UserLevel Level { get; set; } = UserLevel.Operater;

        public static bool IsRunning { get; set; } = false;

        public static string Floor { get; set; }

        public static string Line { get; set; }

        public static string ProjectCode { get; set; }
    }
}
