namespace Advent.Days
{
    internal class Day03
    {
        private int Score(char x) => x % 32 + (char.IsUpper(x) ? 26 : 0);

        internal int First(string[] lines)
            => lines.Select(x => x.Chunk(x.Length / 2)).Select(x => Score(x.First().Intersect(x.Last()).First())).Sum();

        internal int Second(string[] lines)
            => lines.Chunk(3).Select(x => Score(x[0].Intersect(x[1]).Intersect(x[2]).First())).Sum();
    }
}