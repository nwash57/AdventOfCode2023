using FluentAssertions;

namespace Day1Puzzle2Tests;

public class ReadDigitsTests
{
	[Theory]
	[InlineData("two", "t2o")]
	[InlineData("eighteight", "e8te8t")]
	[InlineData("twotwo", "t2ot2o")]
	[InlineData("eightwoeightwo", "e8t2oe8t2o")]
	[InlineData("threeight", "t3e8t")]
	[InlineData("twovgtprdzcjjzkq3ffsbcblnpq", "t2ovgtprdzcjjzkq3ffsbcblnpq")]
	[InlineData("two8sixbmrmqzrrb1seven", "t2o8s6xbmrmqzrrb1s7n")]
	[InlineData("9964pfxmmr474", "9964pfxmmr474")]
	[InlineData("46one", "46o1e")]
	[InlineData("7fvfourgkfkkbloneeightdrfscspgkdrmzzt1", "7fvf4rgkfkkblo1ee8tdrfscspgkdrmzzt1")]
	public void PreprocessTests(string input, string expected)
	{
		var result = input.Preprocess();
		result.Should().Be(expected);
	}

	[Theory]
	[InlineData("two", "")]
	[InlineData("eighteight", "")]
	[InlineData("twotwo", "")]
	[InlineData("eightwoeightwo", "")]
	[InlineData("twovgtprdzcjjzkq3ffsbcblnpq", "3")]
	[InlineData("two8sixbmrmqzrrb1seven", "81")]
	[InlineData("9964pfxmmr474", "9964474")]
	[InlineData("46one", "46")]
	[InlineData("7fvfourgkfkkbloneeightdrfscspgkdrmzzt1", "71")]
	public void ReadDigits_ExtractsNumeric(string input, string expected)
	{
		var result = input.ReadDigits();
		var joined = string.Join("", result);
		joined.Should().Be(expected);
	}

	[Theory]
	[InlineData(22, '2')]
	[InlineData(22, '2', '2')]
	[InlineData(22, '2', '1', '2')]
	[InlineData(14, '1', '2', '3', '4')]
	public void ParseDigits_ExtractsFirstAndLast(int expected, params char[] input)
	{
		var result = input.ParseDigits();
		result.Should().Be(expected);
	}
}