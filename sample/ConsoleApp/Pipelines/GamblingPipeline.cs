using Bogoware.Monads;

namespace Sample.Pipelines;

/// <summary>
/// This pipeline emulate a player who bets at mots 10 times trying to win on black/red
/// at roulette.
/// </summary>
public static class GamblingPipeline
{
	public enum Colors
	{
		Red = 0, Black = 1
	}
	public record Amount(int Value);

	public static void Run(int initialAmount, int numberOfAttempts)
	{
		Task<Result<Amount>> FourBetsInARow() => Result.Success(new Amount(initialAmount))
			.Bind(a => Bet(a, (Colors)Random.Shared.Next(2)))
			.Bind(a => Bet(a, (Colors)Random.Shared.Next(2)))
			.Bind(a => Bet(a, (Colors)Random.Shared.Next(2)))
			.Bind(a => Bet(a, (Colors)Random.Shared.Next(2)));
		
		Console.WriteLine($"You're attempting to win 4 red/black bets in a row in {numberOfAttempts} attempts.");

		var attemptsTasks = new List<Task<Result<Amount>>>();
		for (var i = 0; i < numberOfAttempts; i++)
		{
			attemptsTasks.Add(FourBetsInARow());
		}

		Task.WaitAll(attemptsTasks.ToArray());

		var attempts = attemptsTasks.Select(task => task.Result);

		var messages = attempts.MatchEach(
			win => $"You won {win.Value}",
			$"You lost {initialAmount}");
		
		foreach (var message in messages)
		{
			Console.WriteLine(message);
		}

		var totalWin = (
			from a in attempts.SelectValues()
			select a.Value).Sum();

		Console.WriteLine($"You spent: {initialAmount * numberOfAttempts}");
		Console.WriteLine($"You 'won': {totalWin}");
	}

	private static async Task<Result<Amount>> Bet(Amount amount, Colors color)
	{
		var winColor = (Colors) Random.Shared.Next(2);

		await Task.Delay(Random.Shared.Next(1000));
		
		if (winColor == color)
		{
			Console.WriteLine("   you won :)");
			return Result.Success(new Amount(amount.Value * 2));
		}
		else
		{
			Console.WriteLine("   you lost :(");
			return new LogicError("You lost");
		}
	}

}