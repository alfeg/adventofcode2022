using System.Data;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace aoc;

public partial class Day09 : AdventTest
{
    record Vec(int X, int Y, string? Code = null)
    {
        public Vec Add(Vec other) => this with {X = X + other.X, Y = Y + other.Y};

        public bool IsHorizontal => X != 0 && Y == 0;
        public bool IsVertical => X == 0 && Y != 0;
        public bool IsDiagonal => X != 0 && Y != 0;
    }

    record Position(int X, int Y)
    {
        public Position Add(Vec vec) => this with {X = X + vec.X, Y = Y + vec.Y};

        public Vec GetDirection(Position p)
        {
            var xDiff = p.X - X;
            var yDiff = p.Y - Y;

            if (Math.Abs(xDiff) > 1) xDiff /= Math.Abs(xDiff);
            if (Math.Abs(yDiff) > 1) yDiff /= Math.Abs(yDiff);

            return new Vec(xDiff, yDiff);
        }

        public bool IsNearby(Position p)
        {
            if (X >= p.X - 1 && X <= p.X + 1)
            {
                if (Y >= p.Y - 1 && Y <= p.Y + 1)
                {
                    return true;
                }
            }

            return false;
        }
    };

    record Command(Vec Direction, int Iterations, int Line);

    [TestCase("day9.sample", 2, ExpectedResult = 13)]
    [TestCase("day9.input", 2, ExpectedResult = 6284)]
    [TestCase("day9.sample", 10, ExpectedResult = 1)]
    [TestCase("day9.sample2", 10, ExpectedResult = 36)]
    [TestCase("day9.input", 10, ExpectedResult = 2661)]
    public int Part1and2(string inputName, int ropeSize)
    {
        var line = 0;
        var lines = GetLines(inputName)
            .Select(l =>
            {
                var s = l.Split(" ");
                return new Command(s[0] switch
                {
                    "R" => new Vec(1, 0, s[0]),
                    "L" => new Vec(-1, 0, s[0]),
                    "U" => new Vec(0, 1, s[0]),
                    "D" => new Vec(0, -1, s[0]),
                }, int.Parse(s[1]), line++);
            }).ToList();

        (int min, int max) width = (0, 0);
        (int min, int max) height = (0, 0);

        var rope = Enumerable.Range(0, ropeSize).Select(_ => new Position(0, 0)).ToList();

        var visits = new HashSet<Position>();
        visits.Add(rope.Last());
        foreach (var command in lines)
        {
            for (int i = 0; i < command.Iterations; i++)
            {
                rope[0] = rope[0].Add(command.Direction);

                for (int r = 1; r < rope.Count; r++)
                {
                    var tail = rope[r];
                    var head = rope[r - 1];

                    if (tail.IsNearby(head))
                    {
                        break;
                    }

                    var direction = tail.GetDirection(head);
                    rope[r] = tail.Add(direction);
                }

                visits.Add(rope.Last());
            }
            
            if (inputName.Contains("sample"))
            {
                DumpConsole(rope, 0);
            }
            
            string DumpConsole(List<Position> positions, int step)
            {
                var sb = new StringBuilder();
                width = (Math.Min(width.min, positions.Min(r => r.X)), Math.Max(width.max, positions.Max(r => r.X)));
                height = (Math.Min(height.min, positions.Min(r => r.Y)), Math.Max(height.max, positions.Max(r => r.Y)));
                sb.AppendLine(
                    $"[ {command.Line} ] Command: {command.Direction.Code} step {step} of {command.Iterations}");
                for (int i = height.min; i <= height.max; i++)
                {
                    for (int j = width.min; j <= width.max; j++)
                    {
                        bool hasRope = false;
                        for (int r = 0; r < positions.Count; r++)
                        {
                            if (positions[r].X == j && positions[r].Y == i)
                            {
                                sb.Append(r == 0 ? 'H' : r == positions.Count - 1 ? "T" : r);
                                hasRope = true;
                                break;
                            }
                        }

                        if (!hasRope) sb.Append(".");
                    }

                    sb.AppendLine();
                }

                Console.WriteLine(sb.ToString());
                Debug.WriteLine(sb.ToString());
                return sb.ToString();
            }
        }

        return visits.Count;
    }
}