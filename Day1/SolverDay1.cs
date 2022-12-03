namespace adventofcode2022.Day1;

internal class SolverDay1 : ISolver
{
    public async IAsyncEnumerable<(int day, int part, object answer)> Solve()
    {
        var elves = new List<long>();
        var max = 0L;

        var currentElf = 0L;
        await foreach (var line in InputReader.ReadInput("Day1/input.txt"))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentElf > max)
                {
                    max = currentElf;
                }

                elves.Add(currentElf);
                currentElf = 0;
            }
            else
            {
                var amount = long.Parse(line);
                currentElf += amount;
            }
        }

        if (currentElf > max)
        {
            max = currentElf;
        }

        yield return (1, 1, max);
        yield return (1, 2, elves.OrderByDescending(e => e).Take(3).Sum());
    }
}