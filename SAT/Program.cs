using System.Diagnostics;
using SAT;


var parser = new Parser();
var cnf = parser.ParseFile(args[0]);
var model = Solver.SolveSat(cnf);

Parser.WriteModelToConsole(model);
