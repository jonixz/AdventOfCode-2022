namespace Advent.Days
{
    internal class Day02
    {
        internal int First(string[] lines)
        {
            return -1;
        }

        internal int Second(string[] lines) => lines.Select(x => (Elf: x[0] - 65, Cmd: x[2]))
            .Select(x => (x.Cmd == 'Y' ? (x.Elf + 3) : ((x.Elf + (x.Cmd == 'X' ? 2 : 1)) % 3)) + 1 + (x.Cmd == 'Z' ? 6 : 0))
            .Sum();
    }
}
