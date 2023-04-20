using System.Collections.Generic;
using System.Linq;

namespace SAT;

public class Cnf
{
    public int CountVars { get; }

    private List<Clause> _clauses;
    private List<int> _variable;

    public Cnf(List<Clause> clauses)
    {
        _clauses = new List<Clause>(clauses);
        _variable = new List<int>();
        foreach (var clause in clauses)
        {
            foreach (var literal in clause.Variable)
            {
                if (literal < 0)
                {
                    if (!_variable.Contains(-literal))
                    {
                        _variable.Add(-literal);
                    }
                }
                else
                {
                    if (!_variable.Contains(literal))
                    {
                        _variable.Add(literal);
                    }
                }
            }
        }

        CountVars = _variable.Count;
    }

    public Cnf(List<Clause> clauses, int countVars) : this(clauses)
    {
        CountVars = countVars;
    }

    public Clause? UnitClause => _clauses.FirstOrDefault(clause => clause.IsUnitClause);

    public int PureLiteral => GetPureLiteral();

    public bool IsEmpty => _clauses.Count == 0;

    public bool HasEmptyClause => _clauses.Any(clause => clause.IsEmpty);

    public Cnf PropagateUnit(int notAssigned)
    {
        var simplifiedClauses = new List<Clause>(_clauses);

        foreach (var clause in _clauses)
        {
            if (clause.Variable.Contains(notAssigned))
            {
                simplifiedClauses.Remove(clause);
            }
            else if (clause.Variable.Contains(-notAssigned))
            {
                simplifiedClauses.Find(item => item == clause).Variable.Remove(-notAssigned);
            }
        }

        return new(simplifiedClauses);
    }

    public int GetPureLiteral() //литерал с одной полярностью
    {
        foreach (var uniqueLiteral in _variable)
        {
            var isPure = true;
            foreach (var clause in _clauses)
            {
                if (clause.Variable.Contains(-uniqueLiteral))
                {
                    isPure = false;
                    break;
                }
            }

            if (isPure)
            {
                return uniqueLiteral;
            }

            isPure = true;
            foreach (var clause in _clauses)
            {
                if (clause.Variable.Contains(uniqueLiteral))
                {
                    isPure = false;
                    break;
                }
            }

            if (isPure)
            {
                return -uniqueLiteral;
            }
        }

        return 0;
    }

    public Cnf
        EliminatePureLiteral(int pureLiteral) //удаление предложений, в которые входит литерал с одной полярностью
    {
        _clauses.RemoveAll(clause => clause.Variable.Contains(pureLiteral));
        return new(_clauses);
    }

    public int GetLiteral() => _variable.First();

    public Cnf InsertValueToLiteral(int literal, bool value)
    {
        var clausesDeepClone = _clauses.Select(clause => (Clause)clause.Clone()).ToList();

        var simplifiedClauses = new List<Clause>(clausesDeepClone);

        foreach (var clause in clausesDeepClone)
        {
            if (clause.Variable.Contains(literal))
            {
                if (literal > 0 && value)
                {
                    simplifiedClauses.Remove(clause);
                }
                else
                    clause.Variable.Remove(literal);
            }

            if (clause.Variable.Contains(-literal))
            {
                if (literal > 0 && value)
                    clause.Variable.Remove(-literal);
                else
                {
                    simplifiedClauses.Remove(clause);
                }
            }
        }

        return new(simplifiedClauses);
    }

    public override string ToString()
    {
        var builder = "";

        const string separator = " /\\ ";
        foreach (var clause in _clauses)
        {
            builder += clause;
            builder += separator;
        }


        builder = builder.Substring(0, builder.Length - separator.Length);
        return builder;
    }
}