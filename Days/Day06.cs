namespace Advent.Days
{
    internal class Day06
    {
        private int FindStart(string input, int length) => input.Skip(length)
                .Select((value, i) => (value, i: i + length))
                .TakeWhile(x => input.Take(new Range(x.i - length, x.i)).Distinct().Count() != length).Count() + length;

        public int First(string[] lines) => FindStart(lines[0], 4);

        internal int Second(string[] lines) => FindStart(lines[0], 14);
    }
}
