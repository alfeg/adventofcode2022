// https://adventofcode.com/2022/day/10

using NUnit.Framework;

namespace aoc;

public partial class Day10 : AdventTest
{
    [TestCase("day10.sample", ExpectedResult = 13140)]
    [TestCase("day10.input", ExpectedResult = 16880)]
    public int Part1(string inputName)
    {
        var lines = GetLines(inputName)
            .Select(line =>
            {
                var cmd = line.Split(" ");
                var name = cmd[0];
                var value = 0;
                if (name == "addx")
                {
                    value = int.Parse(cmd[1]);
                }

                return new Command(name, value, name == "noop" ? 1 : 2);
            })
            .GetEnumerator();

        var signalSum = 0;
        var X = 1;

        lines.MoveNext();
        var cmd = lines.Current;
        var cmdCyclesLeft = cmd.cycles;
        int cycle = 0;
        while (true)
        {if(cycle > 0 && cycle % 40 == 0) Console.WriteLine();

            if (Math.Abs(cycle % 40 - X) < 2)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }
            cycle++;
            cmdCyclesLeft--;

            if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
            {
                signalSum += cycle * X;
            }
            
            switch (cmd.cmd)
            {
                case "noop" when cmdCyclesLeft > 0:
                case "addx" when cmdCyclesLeft > 0:
                    continue;
                case "addx":
                    X += cmd.value;
                    break;
            }

            if (!lines.MoveNext())
            {
                break;
            }

            cmd = lines.Current;
            cmdCyclesLeft = cmd.cycles;
        }

        return signalSum;
    }

    record Command(string cmd, int value, int cycles);
}