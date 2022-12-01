using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022.Day1;

internal static class SolverDay1
{
    public static async Task Solve()
    {
        var elves = new List<long>();
        var max = 0L;
        using var fs = File.OpenRead("Day1/input.txt");
        using var sr = new StreamReader(fs);

        var currentElf = 0L;
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();
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
            currentElf = 0;
        }

        Console.WriteLine(max);
        Console.WriteLine(elves.OrderByDescending(e => e).Take(3).Sum());
    }    
}
