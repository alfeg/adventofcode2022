// https://adventofcode.com/2022/day/12

using NUnit.Framework;

namespace aoc;

public partial class Day12 : AdventTest
{
    record Node(int X, int Y, int height, char Code)
    {
        public List<Node> Links { get; set; } = new();
        public int DijkstraDistance { get; set; }

        public virtual bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    [TestCase("day12.sample", ExpectedResult = 31)]
    [TestCase("day12.input", ExpectedResult = 447)]
    public int Part1(string inputName)
    {
        var lines = GetLines(inputName).ToList();
        var nodes = HashSet(lines, out var start, out var end);

        DijkstraAlgorithm(nodes, start);
        
        return end.DijkstraDistance;
    }
    
    [TestCase("day12.sample", ExpectedResult = 29)]
    [TestCase("day12.input", ExpectedResult = 446)]
    public int Part2(string inputName)
    {
        var lines = GetLines(inputName).ToList();
        var nodes = HashSet(lines, out var start, out var end);

        var min = int.MaxValue;

        foreach (var aNode in nodes.Where(n => n.height == 'a'))
        {
            DijkstraAlgorithm(nodes, aNode);
            if (end.DijkstraDistance < min)
            {
                min = end.DijkstraDistance;
            }
        }
        
        return min;
    }

    private static HashSet<Node> HashSet(List<string> lines, out Node? start, out Node? end)
    {
        var xLen = lines.Count;
        var yLen = lines[0].Length;
        var map = new Node[xLen, yLen];
        var nodes = new HashSet<Node>();
        start = null;
        end = null;

        for (var y = 0; y < yLen; y++)
        {
            for (var x = 0; x < xLen; x++)
            {
                var code = lines[x][y];
                var value = (int) code;
                if (value == 'S') value = 'a';
                if (value == 'E') value = 'z';

                map[x, y] = new Node(x, y, (int) value, code);
                if (lines[x][y] == 'S') start = map[x, y];
                if (lines[x][y] == 'E') end = map[x, y];
            }
        }

        Node? Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= xLen || y >= yLen) return null;
            return map[x, y];
        }

        IEnumerable<Node> GetLinks(int x, int y)
        {
            var nodes = new List<Node>
            {
                Get(x - 1, y), Get(x + 1, y), Get(x, y - 1), Get(x, y + 1)
            };
            return nodes.Where(n => n != null);
        }

        for (var y = 0; y < yLen; y++)
        {
            for (var x = 0; x < xLen; x++)
            {
                var node = map[x, y];
                var links = GetLinks(x, y);

                node.Links = links
                    .Where(l => l.height <= node.height + 1)
                    .ToList();

                nodes.Add(node);
            }
        }

        return nodes;
    }

    static void DijkstraAlgorithm(HashSet<Node> graph, Node source)
    {
        var queue = new Queue<Node>();

        foreach (var item in graph)
        {
            item.DijkstraDistance = int.MaxValue;
        }

        queue.Enqueue(source);
        source.DijkstraDistance = 0;

        while (queue.Count != 0)
        {
            var currentNode = queue.Dequeue();

            foreach (var connection in currentNode.Links)
            {
                var potDistance = 1 + currentNode.DijkstraDistance;

                if (potDistance < connection.DijkstraDistance)
                {
                    connection.DijkstraDistance = potDistance;
                    queue.Enqueue(connection);
                }
            }
        }
    }
}