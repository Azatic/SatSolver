using SAT;

var filePath = @"/Users/azat/Desktop/aim-100-1_6-no-1.cnf.txt";

if (!File.Exists(filePath))
    throw new ArgumentException("File not exists");

var parser = new Parser();
var cnf = parser.ParseFile(filePath);
var model = Solver.SolveSat(cnf);

Parser.WriteModelToConsole(model);
Console.WriteLine(123);