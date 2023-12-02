Console.WriteLine("=== Advent of Code Day 1 Puzzle 1 ===");


var lines = File.ReadLines("input.txt");
Console.WriteLine($"Read {lines.Count()} lines");
var sum = lines.Select(line => line.ReadDigits().ParseDigits())
	.Sum();

Console.WriteLine($"Sum is {sum}");

public static class Calculator
{
	public static char[] ReadDigits(this string line)
	{
		return line.Where(c => c is >= '0' and <= '9').ToArray();
	}

	public static int ParseDigits(this char[] digits)
	{
		return int.Parse($"{digits[0]}{digits[^1]}");
	}
}