namespace Advent.Days
{
    internal class Day05
    {
        private (IEnumerable<(int Count, int From, int To)> operations, IList<Stack<char>> stacks) Parse(string[] lines)
        {
            int splitIndex = Array.FindIndex(lines, x => string.IsNullOrWhiteSpace(x));
            var stackText = lines[Range.EndAt(splitIndex)];

            List<Stack<char>> stacks = new();
            for (int x = 0; x < stackText.Last().Length; x++)
            {
                if (!char.IsDigit(stackText.Last()[x]))
                    continue;

                stacks.Add(new());
                for (int y = stackText.Length - 2; y >= 0 ; y--)
                {
                    var value = stackText[y][x];
                    if (char.IsLetter(value))
                        stacks.Last().Push(value);
                }
            }
            return (lines[Range.StartAt(splitIndex + 1)].Select(x => x.Split(' ')).Select(x => (int.Parse(x[1]), int.Parse(x[3]) - 1, int.Parse(x[5]))), stacks);
        }

        internal string First(string[] lines)
        {
            var (operations, stacks) = Parse(lines);

            foreach (var operation in operations)
                for (int i = 0; i < operation.Count; i++)
                    stacks[operation.To].Push(stacks[operation.From].Pop());

            return string.Join("", stacks.Select(x => x.First()));
        }

        internal string Second(string[] lines)
        {
            var (operations, stacks) = Parse(lines);
            foreach (var operation in operations)
            {
                Stack<char> temp = new();
                for (int i = 0; i < operation.Count; i++)
                    temp.Push(stacks[operation.From].Pop());
                while (temp.Count() > 0)
                    stacks[operation.To].Push(temp.Pop());
            }

            return string.Join("", stacks.Select(x => x.First()));
        }
    }
}
