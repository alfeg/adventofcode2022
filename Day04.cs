using NUnit.Framework;

namespace aoc;

public class Day04 : AdventTest
{
    [TestCase("day4.sample", ExpectedResult = 2)]
    [TestCase("day4.input", ExpectedResult = 644)]
    public int Part1(string inputName)
    {
        return GetPairs(inputName)
            .Count(p => p.A.FullOverlaps(p.B) || p.B.FullOverlaps(p.A));
    }

    [TestCase("day4.sample", ExpectedResult = 4)]
    [TestCase("day4.input", ExpectedResult = 926)]
    public int Part2(string inputName)
    {
        return GetPairs(inputName)
            .Count(p => p.A.AnyOverlap(p.B) || p.B.AnyOverlap(p.A));
    }

    IEnumerable<(Range A, Range B)> GetPairs(string input)
    {
        return GetLines(input).Select(line =>
        {
            var split = line.Split(',');

            Range Parse(string item)
            {
                var s = item.Split('-');
                return new Range(int.Parse(s[0]), int.Parse(s[1]));
            }

            return (Parse(split[0]), Parse(split[1]));
        });
    }
}

record Range(int a, int b)
{
    public bool FullOverlaps(Range r)
    {
        return this.a <= r.a && this.b >= r.b;
    }

    public bool AnyOverlap(Range r)
    {
        // ----a------b---
        //r-------a------b
        
        // ----a------b---
        //r---a-----b------
        return r.a <= a && r.b >= a || r.b <= b && r.b >= b;
    }
}