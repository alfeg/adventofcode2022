using System.Text.RegularExpressions;
using NUnit.Framework;

namespace aoc;

public partial class Day05 : AdventTest
{
    [TestCase("day5.sample", ExpectedResult = "CMZ")]
    [TestCase("day5.input", ExpectedResult = "QPJPLMNNR")]
    public string Part1(string inputName)
    {
        return CrateMover(inputName, CrateMover9000);
    }

    private string CrateMover(string inputName, Action<List<Stack<char>>, Command> moveCommand)
    {
        bool setupState = true;

        var stacks = new List<Stack<char>>();
        var stacksDefinition = new List<string>();
        foreach (var line in GetLines(inputName))
        {
            if (setupState)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    setupState = false;
                    stacksDefinition.Reverse();
                    var crates = stacksDefinition[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    stacks.AddRange(Enumerable.Range(0, crates.Length).Select(i => new Stack<char>()));

                    foreach (var definition in stacksDefinition.Skip(1))
                    {
                        int i = 0;
                        foreach (var crate in definition.AsEnumerable().Chunk(4))
                        {
                            if (crate[1] != ' ')
                            {
                                stacks[i].Push(crate[1]);
                            }

                            i++;
                        }
                    }
                }
                else
                {
                    stacksDefinition.Add(line);
                }
            }
            else
            {
                var match = CommandRx().Match(line);
                if (!match.Success) throw new NotSupportedException();
                var cmd = new Command(
                    int.Parse(match.Groups["count"].Value),
                    int.Parse(match.Groups["from"].Value) - 1,
                    int.Parse(match.Groups["to"].Value) - 1);

                moveCommand(stacks, cmd);
            }
        }

        return new string(stacks.Select(s => s.Peek()).ToArray());
    }

    private static void CrateMover9000(List<Stack<char>> stacks, Command cmd)
    {
        for (int i = 0; i < cmd.Count; i++)
        {
            stacks[cmd.To].Push(stacks[cmd.From].Pop());
        }
    }

    [GeneratedRegex(@"move (?<count>\d+) from (?<from>\d+) to (?<to>\d+)", RegexOptions.IgnoreCase)]
    private static partial Regex CommandRx();

    record Command(int Count, int From, int To);

    [TestCase("day5.sample", ExpectedResult = "MCD")]
    [TestCase("day5.input", ExpectedResult = "BQDNWJPVJ")]
    public string Part2(string inputName)
    {
        return CrateMover(inputName, CrateMover9001);
    }

    private static void CrateMover9001(List<Stack<char>> stacks, Command cmd)
    {
        var move = new List<char>();
        for (int i = 0; i < cmd.Count; i++)
        {
            move.Insert(0, stacks[cmd.From].Pop());
        }

        move.ForEach(c => stacks[cmd.To].Push(c));
    }
}