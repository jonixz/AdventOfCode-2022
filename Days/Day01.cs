namespace Advent.Days
{
    internal class Day01
    {
        private int Sum(string lines, int count) => lines
            .Split("\r\n\r\n")
            .Select(x => x.Split("\r\n")
                .Sum(x => int.Parse(x)))
            .OrderByDescending(x => x)
            .Take(count)
            .Sum();

        internal int First(string lines) => Sum(lines, 1);

        internal int Second(string lines) => Sum(lines, 3);
    }
}
