using System.Reflection;

string today = DateTime.Today.ToString("dd");
today = "10";
var day = Assembly.GetExecutingAssembly().CreateInstance($"Advent.Days.Day{today}");
if (day == null)
{
    Console.WriteLine($"Day{today} not found.");
    Console.ReadLine();
    return;
}
object inputLines = File.ReadAllLines($"Input/{day.GetType().Name}.txt");
object inputLinesTest = File.ReadAllLines($"Input/{day.GetType().Name}test.txt");
object input = File.ReadAllText($"Input/{day.GetType().Name}.txt");
object inputTest = File.ReadAllText($"Input/{day.GetType().Name}test.txt");

MethodInfo first = day.GetType().GetMethod("First", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
MethodInfo second = day.GetType().GetMethod("Second", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

Console.WriteLine(day.GetType().Name);
Console.WriteLine(first.Invoke(day, new[] { first.GetParameters().First().ParameterType == typeof(string[]) ? inputLinesTest : inputTest }));
Console.WriteLine(first.Invoke(day, new[] { first.GetParameters().First().ParameterType == typeof(string[]) ? inputLines : input }));
Console.WriteLine();
Console.WriteLine(second.Invoke(day, new[] { second.GetParameters().First().ParameterType == typeof(string[]) ? inputLinesTest : inputTest }));
Console.WriteLine(second.Invoke(day, new[] { second.GetParameters().First().ParameterType == typeof(string[]) ? inputLines : input }));
Console.ReadLine();