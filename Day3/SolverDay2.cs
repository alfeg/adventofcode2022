namespace adventofcode2022.Day3;

internal class SolverDay3 : ISolver
{
    private const string PriorityLine ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public async IAsyncEnumerable<(int day, int part, object answer)> Solve()
    {
        var total = 0;

        var accumulator = new Dictionary<char, int>();
        await foreach (var line in InputReader.ReadInput("Day3/input.txt"))
        {
            var compartment1 = line.Substring(0, line.Length / 2).Distinct();
            var compartment2 = line.Substring(line.Length / 2).Distinct().ToArray();

            foreach (var comp1Item in compartment1.Distinct())
            {
                accumulator[comp1Item] = compartment2.Count(c => c == comp1Item);
            }

            var singleType = accumulator.SingleOrDefault(a => a.Value == 1);
            total += PriorityLine.IndexOf(singleType.Key) + 1;
            accumulator.Clear();
        }

        yield return (3, 1, total);
    }
}