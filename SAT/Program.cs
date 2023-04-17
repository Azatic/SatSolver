using System.Diagnostics;
using SAT;

var filePath = @"/Users/azat/Desktop/aim-100-1_6-no-1.cnf.txt";

if (!File.Exists(filePath))
    throw new ArgumentException("File not exists");

var parser = new Parser();
var cnf = parser.ParseFile(filePath);
var model = Solver.SolveSat(cnf);

Parser.WriteModelToConsole(model);
Console.WriteLine(123);

for (int i = 0; i < 100; i++)
{
    var filePath1 = @"/Users/azat/Desktop/aim-100-1_6-no-1.cnf.txt";
    var filePath2 = @"/Users/azat/Desktop/aim-50-1_6-yes1-4.cnf.txt";
    var sw = new Stopwatch();
    sw.Start();
    var cnf1 = parser.ParseFile(filePath);
    var cnf2 = parser.ParseFile(filePath);
    var model1 = Solver.SolveSat(cnf1);
    var model2 = Solver.SolveSat(cnf2);
    sw.Stop();
    Console.WriteLine(sw.ElapsedMilliseconds);
}