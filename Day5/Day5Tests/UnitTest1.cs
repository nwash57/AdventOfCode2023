using FluentAssertions;

namespace Day5Tests;

public class MapTests
{
	[Fact]
	public void IndexingWorks()
	{
		var almanac = TestData.TestAlmanac;

		almanac["seed", "soil", 0].Should().Be(0);
		almanac["seed", "soil", 1].Should().Be(1);
		almanac["seed", "soil", 49].Should().Be(49);
		almanac["seed", "soil", 50].Should().Be(52);
		almanac["seed", "soil", 51].Should().Be(53);
		almanac["seed", "soil", 96].Should().Be(98);
		almanac["seed", "soil", 98].Should().Be(50);
		almanac["seed", "soil", 99].Should().Be(51);
	}
}

public class ParserTests
{
	[Fact]
	public void ShouldParseSeeds()
	{
		var expected = TestData.TestAlmanac;
		var almanac = Parser.Parse("test.txt");

		almanac
			.Should()
			.BeEquivalentTo(expected);
	}
}

public static class TestData
{
	public static Almanac TestAlmanac => new Almanac(
		new List<long> { 79, 14, 55, 13 },
		new List<Map>
		{
			new(
				"seed",
				"soil",
				new()
				{
					new(98, 50, 2),
					new(50, 52, 48)
				}),
			new(
				"soil",
				"fertilizer",
				new()
				{
					new(15, 0, 37),
					new(52, 37, 2),
					new(0, 39, 15)
				}),
			new(
				"fertilizer",
				"water",
				new()
				{
					new(53, 49, 8),
					new(11, 0, 42),
					new(0, 42, 7),
					new(7,57, 4)
				}),
			new(
				"water",
				"light",
				new()
				{
					new(18,88, 7),
					new(25,18, 70)
				}),
			new(
				"light",
				"temperature",
				new()
				{
					new(77,45, 23),
					new(45,81, 19),
					new(64,68, 13)
				}),
			new(
				"temperature",
				"humidity",
				new()
				{
					new(69,0, 1),
					new(0,1, 69)
				}),
			new(
				"humidity",
				"location",
				new()
				{
					new(56,60, 37),
					new(93,56, 4)
				})
		});
}