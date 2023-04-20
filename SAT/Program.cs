using System.Diagnostics;
using SAT;


// var filePath = @"/Users/azat/Desktop/aim-100-1_6-no-1.cnf.txt";
// var parser = new Parser();
// for (int i = 0; i < 10; i++)
// {
//     var cnf1 = parser.ParseFile(filePath);
//     var model1 = Solver.SolveSat(cnf1);
// }
//
// for (int i = 0; i < 50; i++)
// {
//     var sw = new Stopwatch();
//     sw.Start();
//     var cnf1 = parser.ParseFile(filePath);
//     var model1 = Solver.SolveSat(cnf1);
//     sw.Stop();
//     Console.WriteLine(sw.ElapsedMilliseconds);
//     Console.Write(",");
// }

var parser = new Parser();
var cnf = parser.ParseFile(args[0]);
var model = Solver.SolveSat(cnf);

Parser.WriteModelToConsole(model);