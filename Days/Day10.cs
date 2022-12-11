namespace Advent.Days
{
    internal class Day10
    {
        internal int First(string[] lines) => GetCycles(lines)
            .Where(x => x.Cycle == 20 || ((x.Cycle - 20) % 40) == 0)
            .Select(x => x.Cycle * x.Register)
            .Sum();

        internal void Second(string[] lines) => GetCycles(lines)
            .Select(x => (Action)((x.Cycle % 40) == 0 ? Console.WriteLine : () => Console.Write((x.Cycle % 40) - 1 >= (x.Register - 1) && (x.Cycle % 40) - 1 <= (x.Register + 1) ? "#" : "."))).ToList()
            .ForEach(x => x.Invoke());

        private IEnumerable<(int Cycle, int Register)> GetCycles(string[] lines, int cycle = 0, int register = 1)
        {
            foreach (var line in lines)
            {
                if (line == "noop")
                    yield return (++cycle, register);
                else
                {
                    for (int i = 0; i < 2; i++)
                        yield return (++cycle, register);
                    register += int.Parse(line.Split(' ')[1]);
                }
            }
        }
    }
}
