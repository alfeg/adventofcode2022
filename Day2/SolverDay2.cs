namespace adventofcode2022.Day2;

internal class SolverDay2 : ISolver
{
    enum Game
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    };

    public async IAsyncEnumerable<(int day, int part, object answer)> Solve()
    {
        var totalScore = 0;
        var totalScore2 = 0;

        await foreach (var line in InputReader.ReadInput("Day2/input.txt"))
        {
            var round1 = (Parse(line[0]), Parse(line[2]));
            totalScore += GetRoundScore(round1);

            var round2 = GetRoundForPart2(line);
            totalScore2 += GetRoundScore(round2);
        }

        yield return (2, 1, totalScore);
        yield return (2, 2, totalScore2);

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
            _ => throw new NotSupportedException("Cannot parse: "  + c)
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
}