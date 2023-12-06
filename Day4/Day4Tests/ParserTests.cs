using FluentAssertions;

namespace Day4Tests;

public class AccumulatorTests
{
	[Fact]
	public void Accumulate_ShouldReturnAccumulatedScore()
	{
		var cards = new Card[]
		{
			new(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
			new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
			new(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
			new(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
			new(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
			new(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }),
		};

		var accumulatedCards = cards.Accumulate();

		accumulatedCards.Count().Should().Be(30);
	}
}

public class CardScoreTests
{
	[Theory]
	[InlineData(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }, 8)]
	[InlineData(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }, 2)]
	[InlineData(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }, 2)]
	[InlineData(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }, 1)]
	[InlineData(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }, 0)]
	[InlineData(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }, 0)]
	public void Score_ShouldReturnScore(
		int id,
		int[] winningNumbers,
		int[] numbers,
		int expectedScore)
	{
		var card = new Card(id, winningNumbers, numbers);
		card.Score.Should().Be(expectedScore);
	}

	[Fact]
	public void AltScore_ShouldReturn()
	{
		var cards = new List<Card>
		{
			new(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
			new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
			new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
			new(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
			new(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
			new(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
			new(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }),
		};

		var newCards = cards[0].AltScore(ref cards);
		newCards.Count().Should().Be(4);

		newCards = cards[1].AltScore(ref cards);
		newCards.Count().Should().Be(4);
	}
}

public class ParserTests
{
	[Fact]
	public void ParseCards_ShouldReturnCards()
	{
		var cards = Parser.ParseCards("test.txt");

		var expected = new Card[]
		{
			new(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
			new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
			new(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
			new(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
			new(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
			new(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 }),
		};

		cards.Should().BeEquivalentTo(expected);
	}
}