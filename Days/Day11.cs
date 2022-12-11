namespace Advent.Days
{
	internal class Day11
	{
		private class Monkey
		{
			private Func<long, long> m_operation;
			private Func<long, int> m_test;

			public long Inspections;
			public Queue<long> Items;
			public long TestValue;

			public Monkey(string input)
			{
				var rows = input.Split("\r\n");
				Items = new (rows[1].Split(':')[1].Split(", ").Select(x => long.Parse(x)));
				m_operation = rows[2].Split('=')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries) switch
				{
					[_, "+", "old"] => (x) => x + x,
					[_, "+", var r] => (x) => x + long.Parse(r),
					[_, "*", "old"] => (x) => x * x,
					[_, "*", var r] => (x) => x * long.Parse(r),
					_				=> throw new Exception(),
				};
				TestValue = long.Parse(rows[3].Split(' ').Last());
				m_test = (x) => (x % TestValue) == 0 
					? int.Parse(rows[4].Split(' ').Last()) 
					: int.Parse(rows[5].Split(' ').Last());
			}

			public IEnumerable<(int Index, long Item)> Turn(Monkey[] monkeys, Func<Monkey[], long, long> postOperation)
			{
				while (Items.TryDequeue(out var item))
				{
					Inspections++;
					var level = postOperation(monkeys, m_operation(item));
					yield return (m_test(level), level);
				}
			}
		}

		private long FindMonkeys(string input, int iterations, Func<Monkey[], long, long> postOperation)
		{
			Monkey[] monkeys = input.Split("\r\n\r\n").Select(x => new Monkey(x)).ToArray();
			for (int i = 0; i < iterations; i++)
				foreach (var monkey in monkeys)
					foreach (var result in monkey.Turn(monkeys, postOperation))
						monkeys[result.Index].Items.Enqueue(result.Item);
			return monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate((x, y) => x * y);
		}

		internal long First(string input) => FindMonkeys(input, 20, (_,x) => x / 3);

		internal long Second(string input) => FindMonkeys(input, 10000, (monkeys, x) => x % (monkeys.Select(m => m.TestValue).Aggregate((i, j) => i * j)));
	}
}
