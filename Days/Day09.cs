namespace Advent.Days
{
    internal static class Day09Extensions
    {
        internal static ref (int x, int y) Add(this ref (int x, int y) left, (int x, int y) right)
        {
            left = (left.x + right.x, left.y + right.y);
            return ref left;
        }
    }

    internal class Day09
    {
        internal int First(string[] lines) => MoveRope(lines, 2);

        internal int Second(string[] lines) => MoveRope(lines, 10);

        private int MoveRope(string[] lines, int ropeLength)
        {
            Dictionary<char, (int x, int y)> dirs = new() { { 'R', (1, 0) }, { 'L', (-1, 0) }, { 'U', (0, 1) }, { 'D', (0, -1) } };
            (int x, int y)[] rope = Enumerable.Repeat((0, 0), ropeLength).ToArray();
            HashSet<(int x, int y)> visited = new() { rope[rope.Length - 1] };
            foreach (var cmd in lines)
            {
                for (var s = 0; s < int.Parse(cmd.TrimStart(cmd[0])); s++)
                {
                    rope[0].Add(dirs[cmd[0]]);
                    for (int i = 1; i < rope.Length; i++)
                    {
                        if ((Math.Abs(rope[i].x - rope[i - 1].x) > 1) || (Math.Abs(rope[i].y - rope[i - 1].y) > 1))
                            rope[i].Add(((rope[i - 1].x > rope[i].x ? 1 : rope[i - 1].x < rope[i].x ? -1 : 0), rope[i - 1].y > rope[i].y ? 1 : rope[i - 1].y < rope[i].y ? -1 : 0));
                        else break;
                    }
                    visited.Add(rope[rope.Length - 1]);
                }
            }
            return visited.Count();
        }
    }
}