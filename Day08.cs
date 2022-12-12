// https://adventofcode.com/2022/day/8

using NUnit.Framework;

namespace aoc;

public partial class Day08 : AdventTest
{
    [TestCase("day8.sample", ExpectedResult = 21)]
    [TestCase("day8.input", ExpectedResult = 1684)]
    public int Part1(string inputName)
    {
        var lines = GetLines(inputName).ToList();
        var xLen = lines.Count;
        var yLen = lines[0].Length;

        var forest = new Tree[xLen, yLen];

        for (var x = 0; x < xLen; x++)
        {
            var row = lines[x];
            for (var y = 0; y < yLen; y++)
            {
                forest[x, y] = new Tree(x, y, int.Parse(row[y].ToString()));
            }
        }

        for (var x = 0; x < xLen; x++)
        {
            int max = -1;
            for (var y = 0; y < yLen; y++)
            {
                max = ViewAtTree(forest[x, y], max, View.LR);
            }

            max = -1;
            for (var y = yLen - 1; y >= 0; y--)
            {
                max = ViewAtTree(forest[x, y], max, View.RL);
            }
        }
        
        for (var y = 0; y < yLen; y++)
        {
            int max = -1;
            for (var x = 0; x < xLen; x++)
            {
                max = ViewAtTree(forest[x, y], max, View.TB);
            }

            max = -1;
            for (var x = xLen - 1; x >= 0; x--)
            {
                max = ViewAtTree(forest[x, y], max, View.BT);
            }
        }
        
        int ViewAtTree(Tree tree, int max, View view)
        {
            if (tree.Height > max)
            {
                tree.State[view] = 1;
                max = tree.Height;
            }
            else
            {
                tree.State[view] = 0;
            }

            return max;
        }

        var total = 0;
        for (var x = 0; x < xLen; x++)
        {
            for (var y = 0; y < yLen; y++)
            {
                total += forest[x, y].Visible;
            }
        }

        return total;
    }
    
    [TestCase("day8.sample", ExpectedResult = 8)]
    [TestCase("day8.input", ExpectedResult = 486540)]
    public int Part2(string inputName)
    {
        var lines = GetLines(inputName).ToList();
        var xLen = lines.Count;
        var yLen = lines[0].Length;

        var forest = new Tree[xLen, yLen];

        for (var x = 0; x < xLen; x++)
        {
            var row = lines[x];
            for (var y = 0; y < yLen; y++)
            {
                forest[x, y] = new Tree(x, y, int.Parse(row[y].ToString()));
            }
        }

        for (var x = 1; x < xLen - 1; x++)
        {
            for (var y = 1; y < yLen - 1; y++)
            {
                var tree = forest[x, y];
                tree.State[View.LR] = CountTrees(tree, View.LR);
                tree.State[View.RL] = CountTrees(tree, View.RL);
                tree.State[View.TB] = CountTrees(tree, View.TB);
                tree.State[View.BT] = CountTrees(tree, View.BT);
            }
        }

        int CountTrees(Tree tree, View view)
        {
            int max = tree.Height;
            int count = 0;
            do
            {
                var next = Next(tree, view);
                if (next == null)
                {
                    break;
                }

                count++;

                if (next.Height >= max)
                {
                    break;
                }

                tree = next;
            } while (true);

            return count;
        }
        
        Tree? Next(Tree tree, View view)
        {
            return view switch
            {
                View.LR => tree.X < xLen - 1 ? forest[tree.X + 1, tree.Y] : null,
                View.RL => tree.X > 0 ? forest[tree.X - 1, tree.Y] : null,
                View.TB => tree.Y < yLen - 1 ? forest[tree.X, tree.Y + 1] : null,
                View.BT => tree.Y > 0 ? forest[tree.X, tree.Y - 1] : null,
            };
        }

        var max = 0;
        for (var x = 0; x < xLen; x++)
        {
            for (var y = 0; y < yLen; y++)
            {
                var rank = forest[x, y].Rank;
                if (rank > max)
                {
                    max = rank;
                }
            }
        }

        return max;
    }

    enum View
    {
        LR,
        RL,
        TB,
        BT
    }

    record Tree(int X, int Y, int Height)
    {
        public int Visible => State.Any(s => s.Value > 0) ? 1 : 0;
        public int Rank => State.Select(t => t.Value).Aggregate((x, y) => x * y);
        
        public Dictionary<View, int> State { get; } = new()
        {
            [View.LR] = 0,
            [View.RL] = 0,
            [View.TB] = 0,
            [View.BT] = 0,
        };
    }
}