Console.WriteLine("=== Day 2, Puzzle 1 ===");

var games = File.ReadAllLines("input.txt")
	.Select(Parser.ParseGame)
	.ToArray();

Set availableCubes = new Set(14, 13, 12);

var validGames = games
	.Where(g => g.Sets.All(s => s.Blue <= availableCubes.Blue))
	.Where(g => g.Sets.All(s => s.Green <= availableCubes.Green))
	.Where(g => g.Sets.All(s => s.Red <= availableCubes.Red));

var idSum = validGames.Sum(g => g.Id);

Console.WriteLine("(Puzzle 1) Sum of valid game IDs: " + idSum);

var minSets = games
	.Select(
		g => new Set(
			g.Sets.Select(s => s.Blue).Max(),
			g.Sets.Select(s => s.Green).Max(),
			g.Sets.Select(s => s.Red).Max()));

var powers = minSets.Select(set => set.Blue * set.Green * set.Red);
var minPowerSum = powers.Sum();

Console.WriteLine("(Puzzle 2) Sum of minimum powers: " + minPowerSum);

public record Set(int Blue = 0, int Green = 0, int Red = 0);
public record Game(int Id, Set[] Sets);

public static class Parser
{
	public static string[] SplitAndTrim(this string str, char c) =>
		str.Split(c)
			.Select(s => s.Trim())
			.ToArray();

	public static Game ParseGame(string line)
	{
		var gameAndSets = line.SplitAndTrim(':');
		var gameId = int.Parse(gameAndSets[0].SplitAndTrim(' ')[^1]);
		var sets = gameAndSets[1]
			.SplitAndTrim(';')
			.Select(ParseSet)
			.ToArray();

		return new Game(gameId, sets);
	}

	public static Set ParseSet(string setStr) =>
		setStr.SplitAndTrim(',')
			.Select(parts => parts.SplitAndTrim(' '))
			.Select(
				parts => new
				{
					Num = parts[0],
					Color = parts[^1]
				})
			.Aggregate(
				new Set(),
				(set, part) => part.Color switch
				{
					"blue" => set with { Blue = int.Parse(part.Num) },
					"green" => set with { Green = int.Parse(part.Num) },
					"red" => set with { Red = int.Parse(part.Num) },
					_ => throw new Exception("Unknown color")
				});
}
