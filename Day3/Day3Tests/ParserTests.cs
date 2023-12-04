using FluentAssertions;

namespace Day3Tests;

public class GetAdjacentNumbersTests
{
	[Theory]
	[InlineData(1, 3, 467, 35)]
	[InlineData(4, 3, 617)]
	[InlineData(8, 5, 755, 598)]
	public void GetAdjacentNumbers(
		int lineNum,
		int position,
		params int[] expectedValues)
	{
		var schematic = Parser.ParseSchematic("gear-ratio.txt");
		var symbol = schematic.Lines[lineNum]
			.Symbols
			.Single(s => s.Position == position);

		var numbers = symbol.GetAdjacentNumbers(lineNum, ref schematic);

		expectedValues.Should()
			.AllSatisfy(
				v =>
					numbers.SingleOrDefault(n => n.Value == v)
						.Should()
						.NotBeNull());
	}
}

public class HasAdjacentSymbolTests
{
	[Theory]
	[InlineData(0, 20, true)]
	[InlineData(0, 26, true)]
	[InlineData(0, 47, false)]
	[InlineData(0, 78, true)]
	[InlineData(0, 94, true)]
	[InlineData(0, 104, true)]
	[InlineData(0, 132, false)]
	[InlineData(1, 4, false)]
	[InlineData(1, 8, true)]
	[InlineData(1, 16, true)]
	[InlineData(1, 30, true)]
	[InlineData(1, 37, true)]
	[InlineData(1, 58, true)]
	[InlineData(1, 68, true)]
	[InlineData(1, 124, true)]
	[InlineData(2, 21, false)]
	[InlineData(2, 50, true)]
	[InlineData(2, 54, true)]
	[InlineData(2, 78, true)]
	[InlineData(2, 84, false)]
	[InlineData(2, 100, true)]
	[InlineData(2, 105, false)]
	[InlineData(2, 134, true)]
	public void HasAdjacentSymbol(
		int lineNum,
		int position,
		bool expected)
	{
		var schematic = Parser.ParseSchematic("has-adjacent-symbol.txt");
		var number = schematic.Lines[lineNum]
			.Numbers
			.Single(n => n.Position == position);

		var hasAdjacentSymbol = number.HasAdjacentSymbol(lineNum, ref schematic);

		hasAdjacentSymbol.Should().Be(expected);
	}
}

public class ParseLineTests
{
	[Theory]
	[MemberData(nameof(ParseLineTestData))]
	public void ParseLine(string line, Line expected)
	{
		Parser.ParseLine(expected.LineNum, line).Should().BeEquivalentTo(expected);
	}

	public static List<object[]> ParseLineTestData =>
		new List<object[]>
		{
			new object[]
			{
				".479........155..............944.....622..............31.........264.......................532..........................254.........528.....",
				new Line(
					2,
					(Number[])ReadNumbersTests.ReadNumbersTestData[0][1],
					(Symbol[])ReadSymbolsTests.ReadSymbolsTestData[0][1])
			},
			new object[]
			{
				"$.............-...............%.....+...................=....111*.................495.......+.......558..................../..........*.....",
				new Line(
					2,
					(Number[])ReadNumbersTests.ReadNumbersTestData[1][1],
					(Symbol[])ReadSymbolsTests.ReadSymbolsTestData[1][1])
			}
		};
}

public class ReadNumbersTests
{
	[Theory]
	[MemberData(nameof(ReadNumbersTestData))]
	public void ReadNumbers(string line, Number[] expected)
	{
		var actual = Parser.ReadNumbers(line);
		actual.Should().BeEquivalentTo(expected);
	}

	public static List<object[]> ReadNumbersTestData =>
		new List<object[]>
		{
			new object[]
			{
				".479........155..............944.....622..............31.........264.......................532..........................254.........528.....",
				new Number[]
				{
					new(479, 1), new(155, 12), new(944, 29), new(622, 37), new(31, 54), new(264, 65), new(532, 91),
					new(254, 120),
					new(528, 132)
				}
			},
			new object[]
			{
				"..............-...............%.....+...................=....111*.................495.......+.......558..................../..........*.....",
				new Number[]
				{
					new(111, 61), new(495, 82), new(558, 100)
				}
			}
		};
}

public class ReadSymbolsTests
{
	[Theory]
	[MemberData(nameof(ReadSymbolsTestData))]
	public void ReadSymbols(string line, Symbol[] expected)
	{
		var actual = Parser.ReadSymbols(line);
		Assert.Equal(expected, actual);
	}

	public static List<object[]> ReadSymbolsTestData =>
		new List<object[]>
		{
			new object[]
			{
				".479........155..............944.....622..............31.........264.......................532..........................254.........528.....",
				new Symbol[] { }
			},
			new object[]
			{
				"$.............-...............%.....+...................=....111*.................495.......+.......558..................../..........*.....",
				new Symbol[]
				{
					new('$', 0), new('-', 14), new('%', 30), new('+', 36), new('=', 56), new('*', 64), new('+', 92),
					new('/', 123), new('*', 134)
				}
			}
		};
}

public class LastUsedPositionTests
{
	[Fact]
	public void LastUsedPosition_EmptyList_ReturnsNegativeOne()
	{
		var numbers = new List<Number>();
		var actual = numbers.LastUsedPosition();
		Assert.Equal(-2, actual);
	}

	[Fact]
	public void LastUsedPosition_ListWithOneNumber_ReturnsLastUsedNumberPosition()
	{
		var numbers = new List<Number> { new(value: 1, position: 0) };
		var actual = numbers.LastUsedPosition();
		Assert.Equal(0, actual);
	}

	[Fact]
	public void LastUsedPosition_ListWithTwoNumbers_ReturnsLastUsedNumberPosition()
	{
		var numbers = new List<Number>
		{
			// 1...22.... should be 5
			new(value: 1, position: 0),
			new(value: 22, position: 4)
		};
		var actual = numbers.LastUsedPosition();
		Assert.Equal(5, actual);
	}
}

public class AppendDigitTests
{
	[Fact]
	public void Append1_To1_Returns11()
	{
		var existing = new Number(1, 0);
		var expected = new Number(11, 0);
		var list = new List<Number> { existing };

		list.AppendDigit(new Number(1, 1));

		list.Single().Should().BeEquivalentTo(expected);
	}

	[Fact]
	public void Append3_To12_Returns123()
	{
		var existing = new Number(12, 10);
		var expected = new Number(123, 10);
		var list = new List<Number> { existing };

		list.AppendDigit(new Number(3, 12));

		list.Single().Should().BeEquivalentTo(expected);
	}
}