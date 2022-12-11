namespace Advent.Days
{
    internal static class Day08Extensions
    {
        internal static IEnumerable<IEnumerable<int>> Slices(this IList<string> list, int x, int y)
        {
            yield return list.Slice((i) => i.x == x && i.y > y);
            yield return list.Slice((i) => i.x == x && i.y < y).Reverse();
            yield return list.Slice((i) => i.x > x && i.y == y);
            yield return list.Slice((i) => i.x < x && i.y == y).Reverse();
        }

        internal static IEnumerable<int> Slice(this IList<string> list, Func<(int x, int y), bool> predicate)
        {
            for (int y = 0; y < list.Count; y++)
                for (int x = 0; x < list[0].Length; x++)
                    if (predicate((x, y)))
                        yield return list[y][x];
        }

        internal static IEnumerable<int> TakeUntil(this IEnumerable<int> list, Func<int, bool> predicate)
        {
            foreach (int item in list)
            {
                yield return item;
                if (predicate(item)) yield break;
            }
        }
    }

    internal class Day08
    {
        internal int First(string[] input)
        {
            return Enumerable.Range(0, input.Length * input[0].Length)
                .Select(i => Score(i % input[0].Length, i / input.Length))
                .Sum();

            int Score(int x, int y) => input.Slices(x, y).Any(i => i.All(i => i < input[y][x])) ? 1 : 0;
        }

        internal int Second(string[] input)
        {
            return Enumerable.Range(0, input.Length * input[0].Length)
                .Select(i => Score(i % input[0].Length, i / input.Length))
                .Max();

            int Score(int x, int y) => input.Slices(x, y).Select(i => i.TakeUntil(j => j >= input[y][x]).Count()).Aggregate((i, j) => i * j);
        }
    }
}