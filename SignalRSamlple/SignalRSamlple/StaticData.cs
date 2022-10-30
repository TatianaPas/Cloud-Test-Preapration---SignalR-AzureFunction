namespace SignalRSamlple
{
    public static class StaticData
    {
        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";

        public static Dictionary<string, int> DealthyHallowRace;

        static StaticData()
        {
            DealthyHallowRace = new Dictionary<string, int>();
            DealthyHallowRace.Add(Cloak, 0);
            DealthyHallowRace.Add(Stone, 0);
            DealthyHallowRace.Add(Wand, 0);
        }

    }
}
