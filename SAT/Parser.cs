using System.Text;

namespace SAT;

public class Parser
{
     private const char CommentChar = 'c';
    private const string PCnf = "p cnf";
    private const string EndChar = "0";
    private const char DefaultSeparator = ' ';

    private readonly char _separator;

    private const string SAT = "SAT";
    private const string UNSAT = "UNSAT";
    private const char AnswerStartChar = 'v';

    public Parser(char separator)
    {
        _separator = separator;
    }

    public Parser() : this(DefaultSeparator)
    {
    }

    public Cnf ParseFile(string filePath)
    {
        var lines = File.ReadLines(filePath);
        return ParseLines(lines);
    }

    public Cnf ParseText(string text)
    {
        var lines = text.Split('\n');
        return ParseLines(lines);
    }

    private Cnf ParseLines(IEnumerable<string> lines)
    {
        var clauses = new List<Clause>();
        var countClauses = -1;
        var countVars = -1;
        var readLines = 0;
        foreach (var line in lines)
        {
            if (readLines == countClauses)
                break;

            if (line[0] == CommentChar)
                continue;

            if (line.StartsWith(PCnf))
            {
                var splitLine = line.Split();
                countVars = Convert.ToInt32(splitLine[2]);
                countClauses = Convert.ToInt32(splitLine[3]);
                continue;
            }

            var literals = new List<int>();

            foreach (var literal in line.Split(_separator))
            {
                if (literal == EndChar)
                    break;
                
                if (Convert.ToInt16(literal) > countVars)
                    throw new ArgumentException("Too large variable index");

                literals.Add(Convert.ToInt32(literal));
            }

            clauses.Add(new Clause(literals));
            readLines++;
        }

        return new Cnf(clauses, countVars);
    }

    public static void WriteModelToConsole(List<(int, bool)>? model)
    {
        if (model == null)
        {
            Console.WriteLine(UNSAT);
            return;
        }

        var builder = new StringBuilder(SAT);
        builder.Append('\n');
        builder.Append(AnswerStartChar);
        builder.Append(' ');

        foreach (var literalValue in model)
        {
            builder.Append(literalValue.Item2 ? literalValue.Item1.ToString() : $"{literalValue.Item1}");
            builder.Append(' ');
        }

        builder.Append(EndChar);
        Console.WriteLine(builder.ToString());
    }
}