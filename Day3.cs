using NUnit.Framework;

namespace aoc;

public class Day03 : AdventTest
{
    [TestCase("day3.sample", ExpectedResult = 157)]
    [TestCase("day3.input", ExpectedResult = 7581)]
    public int Part1(string inputName)
    {
        return GetLines(inputName)
            .Select(line => FindCommonScore(
                new []
                {
                    line.Substring(0, line.Length / 2),
                    line.Substring(line.Length / 2),
                }
            ))
            .Sum();
    }
    
    [TestCase("day3.sample", ExpectedResult = 70)]
    [TestCase("day3.input", ExpectedResult = 2525)]
    public int Part2(string inputName)
    {
        return GetLines(inputName)
            .Chunk(3)
            .Select(group => FindCommonScore(group.ToArray()))
            .Sum();
    }
    
    private const string PriorityLine ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    int FindCommonScore(string[] inputs)
    {
        foreach (var type in inputs[0].Distinct())
        {
            if (inputs.Distinct().Skip(1).All(i => i.Contains(type)))
            {
                return PriorityLine.IndexOf(type) + 1;
            }
        }

        throw new NotSupportedException();
    }
}