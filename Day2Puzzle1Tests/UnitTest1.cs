using FluentAssertions;

namespace Day2Puzzle1Tests;

public class ParseGameTests
{
	public static IEnumerable<object[]> TestData =>
		new List<object[]>
		{
			new object[]
			{
				"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
				new Game(1, new[] { new Set(3, 0, 4), new Set(6, 2, 1), new Set(0, 2, 0) })
			},
			new object[]
			{
				"Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
				new Game(2, new[] { new Set(1, 2, 0), new(4, 3, 1), new(1, 1, 0) })
			},
			new object[]
			{
				"Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
				new Game(3, new[] { new Set(6, 8, 20), new(5, 13, 4), new(0, 5, 1)})
			},
			new object[]
			{
				"Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
				new Game(4, new[] { new Set(6, 1, 3), new(0, 3, 6), new(15, 3, 14) })
			},
			new object[]
			{
				"Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
				new Game(5, new[] { new Set(1, 3, 6), new(2, 2, 1) })
			}
		};

	[Theory]
	[MemberData(nameof(TestData))]
	public void ParseGame(string input, Game expected)
	{
		var game = Parser.ParseGame(input);
		game.Should().BeEquivalentTo(expected);
	}

	[Theory]
	[InlineData("3 blue, 4 red", 3, 0, 4)]
	[InlineData("1 red, 2 green, 6 blue", 6, 2, 1)]
	[InlineData("2 green", 0, 2, 0)]
	[InlineData("1 blue, 2 green", 1, 2, 0)]
	[InlineData("3 green, 4 blue, 1 red", 4, 3, 1)]
	[InlineData("1 green, 1 blue", 1, 1, 0)]
	public void ParseSet(string input, int b, int g, int r)
	{
		var set = Parser.ParseSet(input);
		set.Should().Be(new Set(b, g, r));
	}
}
