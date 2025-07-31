namespace Capsule.Core
{
    [System.Serializable]
    public class GenreData
    {
        public string Name;
        public string PrimaryBeat;
        public string[] SecondaryBeats;


        public GenreData(string name, string primary, string[] secondary)
        {
            Name = name;
            PrimaryBeat = primary;
            SecondaryBeats = secondary;
        }
    }

    public static class Genres
    {
        public static readonly GenreData[] All = new GenreData[]
        {
            new GenreData("Romance", "Solution", new[]{"Intro", "Hurdle"}),
            new GenreData("Horror", "Failure", new[]{"Twist", "Betrayal"}),
            new GenreData("Mystery", "Twist", new[]{"Betrayal", "Solution"}),
            new GenreData("Comedy", "Clumsiness", new[]{"Twist", "Failure"}),
            new GenreData("Fantasy", "Intro", new[]{"Hurdle", "Solution"}),
            new GenreData("Sci-Fi", "Setting (Intro)", new[]{"Failure", "Ambiguous Solution"}),
            new GenreData("Drama", "Betrayal", new[]{"Hurdle", "Solution"}),
            new GenreData("Action", "Hurdle", new[]{"Failure", "Victory"}),
            new GenreData("Thriller", "Twist", new[]{"Hurdle", "Betrayal"}),
            new GenreData("Slice of Life", "Intro", new[]{"Fun", "Solution"}),
            new GenreData("Tragedy", "Failure", new[]{"Betrayal", "Tragic Solution"}),
            new GenreData("Adventure", "Hurdle", new[]{"Twist", "Solution"}),
        };
    }
}
