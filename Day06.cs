using System.Text.RegularExpressions;
using NUnit.Framework;

namespace aoc;

public partial class Day06 : AdventTest
{
    [TestCase(null, "bvwbjplbgvbhsrlpgdmjqwftvncz", ExpectedResult = 5)]
    [TestCase(null, "nppdvjthqldpwncqszvftbrmjlhg", ExpectedResult = 6)]
    [TestCase(null, "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", ExpectedResult = 10)]
    [TestCase(null, "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", ExpectedResult = 11)]
    [TestCase("day6.input", null, ExpectedResult = 1140)]
    public int Part1(string inputName, string sample)
    {
        var input = string.IsNullOrWhiteSpace(inputName) ? sample : GetLines(inputName).First();

        var window = new Queue<char>(4);

        for (int i = 0; i < input.Length; i++)
        {
            window.Enqueue(input[i]);
            if (window.Count > 4)
            {
                window.Dequeue();
            }

            if (window.Distinct().Count() == 4) return i + 1;
        }

        return -1;
    }
    
    [TestCase("day6.input", ExpectedResult = 3495)]
    public int Part2(string inputName)
    {
        var input = GetLines(inputName).First();

        var window = new Queue<char>(14);

        for (int i = 0; i < input.Length; i++)
        {
            window.Enqueue(input[i]);
            if (window.Count > 14)
            {
                window.Dequeue();
            }

            if (window.Distinct().Count() == 14) return i + 1;
        }

        return -1;
    }
}