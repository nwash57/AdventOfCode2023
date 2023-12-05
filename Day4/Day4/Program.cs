// See https://aka.ms/new-console-template for more information

Console.WriteLine("=== Day 4 ===");

var cards = Parser.ParseCards("input.txt");
var totalScore = cards.Sum(c => c.Score);
Console.WriteLine("(Puzzle 1) Total score: " + totalScore);

var accumulatedCards = cards.Accumulate().Count();
Console.WriteLine("(Puzzle 2) Total accumulated cards: " + accumulatedCards);


public record Card(int Id, int[] WinningNumbers, int[] Numbers)
{
	public int[] Matches => WinningNumbers
		.Intersect(Numbers)
		.ToArray();

	public int Score => Matches
		.Aggregate(
			0,
			(score, _) => score switch
			{
				0 => 1,
				_ => score * 2
			});

	public Card[] AltScore(ref List<Card> cards)
	{
		int numInstances = cards.Count(c => c.Id == Id);
		int numMatches = Matches.Count();
		return cards
			.Where(c => c.Id > Id)
			.Where(c => c.Id <= Id + numMatches)
			.DistinctBy(c => c.Id)
			.SelectMany(
				card => Enumerable
					.Range(0, numInstances)
					.Select(_ => new Card(card.Id, card.WinningNumbers, card.Numbers))
					.ToArray())
			.ToArray();
	}
}

public static class Accumulator
{

	public static IEnumerable<Card> Accumulate(this Card[] cards)
	{
		var accumulatedCards = new List<Card>(cards);
		foreach (var card in cards)
		{
			accumulatedCards.AddRange(card.AltScore(ref accumulatedCards));
		}

		return accumulatedCards;

		// return _cards
		// 	.Aggregate(_cards, (cards, card) => cards
		// 		.Concat(card.AltScore(ref cards))
		// 		.ToArray());

	}
}


public static class Parser
{
	public static Card[] ParseCards(string inputFile)
	{
		return File.ReadLines(inputFile)
			.Select(line => line.Split(':'))
			.Select(
				parts => (
					id: int.Parse(parts[0].Replace("Card ", "")),
					numbers: parts[1].Split('|')))
			.Select(
				parts => new Card(
					parts.id,
					parts.numbers[0].ParseNumberList(),
					parts.numbers[1].ParseNumberList()))
			.ToArray();
	}

	public static int[] ParseNumberList(this string numbers)
	{
		return numbers.Trim()
			.Split(' ')
			.Where(s => !string.IsNullOrEmpty(s))
			.Select(s => int.Parse(s.Trim()))
			.ToArray();
	}
}