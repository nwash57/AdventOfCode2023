// See https://aka.ms/new-console-template for more information

Console.WriteLine("=== Day 5 ===");

var almanac = Parser.Parse("input.txt");

var seedToLocation = almanac.Seeds
	.Select(s => (s, v: almanac["seed", "soil", s]))
	.Select(s => s with { v = almanac["soil", "fertilizer", s.v] })
	.Select(s => s with { v = almanac["fertilizer", "water", s.v] })
	.Select(s => s with { v = almanac["water", "light", s.v] })
	.Select(s => s with { v = almanac["light", "temperature", s.v] })
	.Select(s => s with { v = almanac["temperature", "humidity", s.v] })
	.Select(s => s with { v = almanac["humidity", "location", s.v] })
	.ToArray();

foreach (var (seed, location) in seedToLocation)
{
	Console.WriteLine($"Seed {seed} grows in {location}");
}

var min = seedToLocation.Min(kv => kv.v);
Console.WriteLine("Minimum location is: " + min);

public static class Parser
{
	public static Almanac Parse(string file)
	{
		var lines = File.ReadLines(file).ToArray();
		var seeds = lines[0].Split(' ')[1..]
			.Select(s => long.Parse(s.Trim()))
			.ToList();

		var maps = new List<Map>();

		foreach (var line in lines[1..])
		{
			if (string.IsNullOrEmpty(line)) continue;

			if (line.EndsWith("map:"))
			{
				var sourceDestArr = line.Split(' ')[0].Split("-to-");
				maps.Add(new Map(sourceDestArr[0], sourceDestArr[1], new()));
				continue;
			}

			var rangeArr = line.Split(' ')
				.Select(s => long.Parse(s.Trim()))
				.ToArray();

			maps.Last().Ranges.Add(new(rangeArr[1], rangeArr[0], rangeArr[2]));
		}

		return new Almanac(seeds, maps);
	}
}

public record Almanac(
	List<long> Seeds,
	List<Map> Maps)
{
	public long this[string source, string dest, long i] =>
		Maps.Single(
			m =>
				m.Source.Equals(source)
				&& m.Destination.Equals(dest))[i];
}

public record Map(
	string Source,
	string Destination,
	List<Range> Ranges)
{

	public long this[long i]
	{
		get
		{
			var range = Ranges
				.SingleOrDefault(r => r.Source <= i && i < r.SourceEnd);

			return range is null ? i : range.Destination + (i - range.Source);
		}
	}
}

public record Range(
	long Source,
	long Destination,
	long Length)
{
	public long SourceEnd => Source + Length;
	public long DestinationEnd => Destination + Length;
}