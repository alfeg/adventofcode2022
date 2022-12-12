// https://adventofcode.com/2022/day/2

using NUnit.Framework;

namespace aoc;

public class Day02 : AdventTest
{
    [TestCase("day2.sample", ExpectedResult = 15)]
    [TestCase("day2.input", ExpectedResult = 10816)]
    public int Part1(string inputName)
    {
        var totalScore = 0;
        foreach (var line in GetLines(inputName))
        {
            var round1 = (Parse(line[0]), Parse(line[2]));
            totalScore += GetRoundScore(round1);
        }

        return totalScore;
    }

    [TestCase("day2.sample", ExpectedResult = 12)]
    [TestCase("day2.input", ExpectedResult = 11657)]
    public int Part2(string inputName)
    {
        var totalScore = 0;
        foreach (var line in GetLines(inputName))
        {
            var round = GetRoundForPart2(line);
            totalScore += GetRoundScore(round);
        }

        return totalScore;
    }

    enum Game
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }

    (Game opponent, Game you) GetRoundForPart2(string line)
    {
        var game = Parse(line[0]);

        return line[2] switch
        {
            'X' => (game, Loose()),
            'Y' => (game, game),
            'Z' => (game, Win()),
            _ => throw new NotSupportedException()
        };

        Game Loose() => game switch
        {
            Game.Rock => Game.Scissor,
            Game.Paper => Game.Rock,
            Game.Scissor => Game.Paper,
            _ => throw new ArgumentOutOfRangeException()
        };

        Game Win() => game switch
        {
            Game.Rock => Game.Paper,
            Game.Paper => Game.Scissor,
            Game.Scissor => Game.Rock,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    Game Parse(char c) => c switch
    {
        'A' or 'X' => Game.Rock,
        'B' or 'Y' => Game.Paper,
        'C' or 'Z' => Game.Scissor,
        _ => throw new NotSupportedException("Cannot parse: " + c)
    };

    int GetRoundScore((Game opponentGame, Game you) round)
    {
        var roundScore = GetScore(round.you, round.opponentGame);
        return roundScore + (int) round.you;

        int GetScore(Game a, Game b)
        {
            if (a == b)
            {
                return 3; // DRAW
            }

            return (a, b) switch
            {
                (Game.Paper, Game.Rock) => 6,
                (Game.Rock, Game.Scissor) => 6,
                (Game.Scissor, Game.Paper) => 6,
                _ => 0
            };
        }
    }
}