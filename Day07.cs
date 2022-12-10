using NUnit.Framework;

namespace aoc;

public partial class Day07 : AdventTest
{
    [TestCase("day7.sample", ExpectedResult = 95437)]
    [TestCase("day7.input", ExpectedResult = 1783610)]
    public long Part1(string inputName)
    {
        var vfs = GetVfsContent(inputName);

        return vfs.Where(e => e.Size == 0)
            .Select(e => (
                e.Path,
                vfs.Where(ve => ve.Path.StartsWith(e.Path)).Sum(ve => ve.Size)))
            .Where(a => a.Item2 < 100000)
            .Sum(a => a.Item2);
    }
    
    [TestCase("day7.sample", ExpectedResult = 24933642)]
    [TestCase("day7.input", ExpectedResult = 4370655)]
    public long Part2(string inputName)
    {
        var total = 70000000L;
        var need = 30000000L;
        
        var vfs = GetVfsContent(inputName).ToList();

        var inUse = vfs.Sum(v => v.Size);  // 48381165
        var unused = total - inUse; // 21618835
        var required = need - unused; //8381165
        
        return vfs.Where(e => e.Size == 0)
            .Select(e => (
                e.Path,
                vfs.Where(ve => ve.Path.StartsWith(e.Path)).Sum(ve => ve.Size)))
            .Select(a => a.Item2)
            .Where(a => a > required).MinBy(a => a);
    }
    
    private List<Entry> GetVfsContent(string inputName)
    {
        string cwd = "/";

        var vfs = new List<Entry>();

        foreach (var line in GetLines(inputName))
        {
            if (line.StartsWith("$"))
            {
                var cmd = ParseCommand(line);
                switch (cmd.cmd)
                {
                    case "cd":
                        cwd = ChangeDirectory(cmd, cwd);
                        break;
                    case "ls":
                        //readingOutput = true;
                        break;
                }
            }
            else
            {
                var split = line.Split(" ");
                if (split[0] == "dir")
                {
                    vfs.Add(new Entry(cwd + split[1] + "/", 0));
                }
                else
                {
                    vfs.Add(new Entry(cwd + split[1], long.Parse(split[0])));
                }
            }
        }

        return vfs;
    }

    private static string ChangeDirectory((string cmd, string arg) cmd, string cwd)
    {
        if (cmd.arg == "..")
        {
            return cwd.Substring(0, cwd.TrimEnd('/').LastIndexOf('/')) + "/";
        }
        else if (cmd.arg == "/")
        {
            return "/";
        }
        else
        {
            return cwd + cmd.arg + "/";
        }
    }

    (string cmd, string arg) ParseCommand(string line)
    {
        if (line == "$ ls") return ("ls", null);
        
        var split = line.Substring(2).Split(" ");
        return (split[0], split[1]);
    }

    record Entry(string Path, long Size);
}