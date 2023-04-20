using System.Text;

namespace SAT;

public class Clause
{
    public List<int> Variable { get; }

    public Clause(List<int> literals)
    {
        Variable = new List<int>(literals);
    }

    public bool IsEmpty => Variable.Count == 0;

    public bool IsUnitClause => Variable.Count == 1;

    public int NotAssigned => Variable.Single();

    public object Clone() => new Clause(Variable);

    public override string ToString()
    {
        string builder = "";
        builder += '(';

        const string separator = " \\/ ";
        foreach (var literal in Variable)
        {
            builder += literal;
            builder += separator;
        }

        builder = builder.Substring(0, builder.Length - separator.Length);
        builder += ')';

        return builder;
    }
}