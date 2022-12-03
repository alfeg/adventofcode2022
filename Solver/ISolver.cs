namespace adventofcode2022.Solver;

public interface ISolver
{
    IAsyncEnumerable<(int day, int part, object answer)> Solve();
}