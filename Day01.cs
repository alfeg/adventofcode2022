// https://adventofcode.com/2022/day/1

using NUnit.Framework;

namespace aoc;

public class Day01 : AdventTest
{
    [TestCase("day1.sample", ExpectedResult = 24000)]
    [TestCase("day1.input", ExpectedResult = 71506)]
    public int Part1(string inputName)
    {
        return GetElfs(inputName).Select(e => e.Sum()).Max();
    }
    
    [TestCase("day1.sample", ExpectedResult = 45000)]
    [TestCase("day1.input", ExpectedResult = 209603)]
    public int Part2(string inputName)
    {
        var elves = GetElfs(inputName).Select(e => e.Sum()).ToArray();
        return elves.OrderByDescending(e => e).Take(3).Sum();
    }
    
    IEnumerable<List<int>> GetElfs(string inputName)
    {
        var elf = new List<int>();
        foreach (var item in GetLines(inputName))
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                yield return elf;
                elf = new List<int>();
            }
            else
            {
                elf.Add(int.Parse(item));
            }
        }

        if (elf.Any())
        {
            yield return elf;
        }
    }
}