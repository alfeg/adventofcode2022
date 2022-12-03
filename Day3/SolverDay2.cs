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

            if (accumulator.Any(a => a.Value > 1))
            {
                Console.WriteLine(accumulator);
            }

            total += PriorityLine.IndexOf(singleType.Key) + 1;
            accumulator.Clear();
        }

        yield return (3, 1, total);

        // part 2

        accumulator.Clear();
        total = 0;
        await foreach (var group in ReadGroups())
        {
            foreach (var type in group[0])
            {
                if (group[1].Contains(type) && group[2].Contains(type))
                {
                    total += PriorityLine.IndexOf(type) + 1;
                    break;
                }
            }
        }

        yield return (3, 2, total);

        async IAsyncEnumerable<string[]> ReadGroups()
        {
            var result = new string[3];
            var i = 0;
            await foreach (var line in InputReader.ReadInput("Day3/input.txt"))
            {
                result[i] = new string(line.Distinct().ToArray());
                i++;
                if (i == 3)
                {
                    yield return result;
                    i = 0;
                }
            }
        }
    }
}