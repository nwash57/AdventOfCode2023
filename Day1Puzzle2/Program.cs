Console.WriteLine("=== Advent of Code Day 1 Puzzle 2 ===");

var lines = File.ReadLines("input.txt");

var sum = lines
	.Select(Calculator.ParseFirstAndLastDigitsAsInt)
	.Sum();

Console.WriteLine($"Sum is {sum}");

public static class Calculator
{
	public static int ParseFirstAndLastDigitsAsInt(string line) =>
		line
			.Preprocess()
			.ReadDigits()
			.ParseDigits();


	public static string Preprocess(this string line) =>
		H.NumberStringMap
			.Aggregate(
				line,
				(result, kv) => result.Replace(
					kv.Key,
					$"{kv.Key[0]}{kv.Value}{kv.Key[^1]}"));

	public static char[] ReadDigits(this string line) =>
		line
			.Where(c => c is >= '0' and <= '9')
			.ToArray();

	public static int ParseDigits(this char[] digits) =>
		int.Parse($"{digits[0]}{digits[^1]}");
}

public static class H
{
	public static Dictionary<string, string> NumberStringMap => new()
	{
		["one"] = "1",
		["two"] = "2",
		["three"] = "3",
		["four"] = "4",
		["five"] = "5",
		["six"] = "6",
		["seven"] = "7",
		["eight"] = "8",
		["nine"] = "9"
	};
}