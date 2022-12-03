namespace adventofcode2022;

public class InputReader
{
    public static async IAsyncEnumerable<string> ReadInput(string file)
    {
        await using var fs = File.OpenRead(file);
        using var sr = new StreamReader(fs);
        
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();
            yield return line;
        }
    }
}

