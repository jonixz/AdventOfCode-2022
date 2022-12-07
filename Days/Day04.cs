namespace Advent.Days
{
    internal class Day04
    {
        private IEnumerable<(int x1, int x2, int y1, int y2)> Parse(string[] lines)
            => lines.Select(i => i.Split(','))
                .Select(i => (x: i[0].Split('-'), y: i[1].Split('-')))
                .Select(i => (int.Parse(i.x[0]), int.Parse(i.x[1]), int.Parse(i.y[0]), int.Parse(i.y[1])));

        internal int First(string[] lines)
            => Parse(lines).Where(i => i.x1 >= i.y1 && i.x2 <= i.y2 || i.y1 >= i.x1 && i.y2 <= i.x2)
                .Count();

        internal int Second(string[] lines)
            => Parse(lines).Where(i => i.y1 >= i.x1 && i.y1 <= i.x2 || i.y2 >= i.x1 && i.y2 <= i.x2 || i.x1 >= i.y1 && i.x1 <= i.y2 || i.x2 >= i.y1 && i.x2 <= i.y2)
                .Count();
    }
}
