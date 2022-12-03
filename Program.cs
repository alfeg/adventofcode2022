using adventofcode2022.Day1;
using adventofcode2022.Day2;
using Spectre.Console;

var solvers = new ISolver[]
{
    new SolverDay1(), new SolverDay2()
};

AnsiConsole.Write(new FigletText("Advent Of Code").Color(Color.Green));
AnsiConsole.Write(new FigletText("________2022").Color(Color.Red));

var table = new Table()
    .AddColumn("Day").AddColumn("Part").AddColumn("Answer", c => c.RightAligned());

foreach (var solver in solvers)
{
    await foreach (var answer in solver.Solve())
    {
        table.AddRow(answer.day.ToString(), answer.part.ToString(), answer.answer.ToString());
    }
}

AnsiConsole.Write(table);