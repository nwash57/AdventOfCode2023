Console.WriteLine("=== Advent of Code Day 1 Puzzle 1 ===");

int sum = 0;

Parallel.ForEach(File.ReadLines("Day1Puzzle1/input.txt"), (line, _, _) =>
{
	sum += line.ReadDigits().ParseDigits();
});

Console.WriteLine($"Sum is {sum}");

public static class Calculator
{
	public static char[] ReadDigits(this string line)
		=> line.Where(c => c is >= '0' and <= '9').ToArray();

	public static int ParseDigits(this char[] digits)
	{
		return int.Parse($"{digits[0]}{digits[^1]}");
	}
}

