// See https://aka.ms/new-console-template for more information

Console.WriteLine("=== Day 3 Puzzles ===");
Console.WriteLine("=== Puzzle 1 ===");

var schematic = Parser.ParseSchematic("input.txt");

var allSymbols = schematic.Lines
	.SelectMany(l => l.Symbols);

var partNumbers = schematic.Lines
	.SelectMany((line, lineNum) => line.Numbers
		.Where(n => n.HasAdjacentSymbol(lineNum, ref schematic)))
	.ToArray();

var sumOfPartNumbers = partNumbers.Sum(n => n.Value);
Console.WriteLine("Sum of part numbers: {0}", sumOfPartNumbers);

public class Number(int value, int position)
{
	public int Value { get; set; } = value;
	public int Position { get; } = position;

	public int LastUsedPosition =>
		Position + (Value.ToString().Length - 1);
}

public record Symbol(char Value, int Position);

public record Line(int LineNum, Number[] Numbers, Symbol[] Symbols);

public record Schematic(Line[] Lines);

public static class Parser
{
	public static Schematic ParseSchematic(string filePath)
	{
		var lines = File.ReadLines(filePath)
			.Select((line, i) => ParseLine(i, line))
			.ToArray();

		return new Schematic(lines);
	}

	public static Line ParseLine(int lineNum, string line)
	{
		return new Line(
			lineNum,
			ReadNumbers(line),
			ReadSymbols(line));
	}

	public static Number[] ReadNumbers(string line)
	{
		return line.Select((c, i) => (c, i))
			.Where(n => n.c.IsNumber())
			.Select(n => (v: int.Parse($"{n.c}"), n.i))
			.Select(n => new Number(n.v, n.i))
			.Aggregate(
				new List<Number>(),
				(list, number) =>
					(number.Position - list.LastUsedPosition()) switch
					{
						1 => list.AppendDigit(number),
						_ => list.Append(number).ToList()
					})
			.ToArray();
	}

	public static Symbol[] ReadSymbols(string line)
	{
		return line
			.Select((c, i) => new Symbol(c, i))
			.Where(s => s.Value.IsSymbol())
			.ToArray();
	}

	public static bool IsSymbol(this char c) =>
		c != '.' && (c < '0' || c > '9');

	public static bool IsNumber(this char c) =>
		char.IsNumber(c);

	public static int LastUsedPosition(this List<Number> numbers)
	{
		if (!numbers.Any())
		{
			return -2; // negative 2 ensures number at pos 0 is not skipped
		}

		var length = numbers.Last().Value.ToString().Length;
		var additionalPositions = length - 1;
		return numbers.Last().Position + additionalPositions;
	}

	public static bool HasAdjacentSymbol(
		this Number number,
		int lineNum,
		ref Schematic schematic)
	{
		var applicableLines = schematic.Lines
			.Where(l => Math.Abs(l.LineNum - lineNum) <= 1);
		var adjacentSymbols = applicableLines
			.SelectMany(
				line => line.Symbols
					.Where(s => s.Position >= number.Position - 1)
					.Where(s => s.Position <= number.LastUsedPosition + 1));
		return adjacentSymbols.Any();
	}

	public static List<Number> AppendDigit(this List<Number> numbers, Number n)
	{
		var last = numbers.Last();
		last.Value = int.Parse(
			$"{last.Value}{n.Value}");
		return numbers;
	}
}