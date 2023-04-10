using System.Text;

namespace SAT;

public class Clause
{
    public List<int> Literals { get; }

    public Clause(List<int> literals)
    {
        Literals = new List<int>(literals);
    }

    public bool IsEmpty => Literals.Count == 0;

    public bool IsUnitClause => Literals.Count == 1;

    public int NotAssigned => Literals.Single();

    public object Clone() => new Clause(Literals);

    public override string ToString()
    {
        string builder = "";
        builder+='(';

        const string separator = " \\/ ";
        foreach (var literal in Literals)
        {
            builder+=literal;
            builder+=separator;
        }

        builder = builder.Substring(0, builder.Length - separator.Length);
        builder += ')';

        return builder;
    }
}