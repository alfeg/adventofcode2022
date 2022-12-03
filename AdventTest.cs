namespace aoc;

public class AdventTest
{
    protected IEnumerable<string> GetLines(string name)
    {
        return File.ReadLines($"Inputs/{name}.txt");
    }
}