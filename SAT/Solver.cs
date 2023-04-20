namespace SAT;

public class Solver
{
    private static Dictionary<(int, bool), List<(int, bool)>> Storage = new();
    private static (int, bool) DummyLiteral = new(0, true);

    public static List<(int, bool)>? SolveSat(Cnf cnf)
    {
        if (!DPLL(cnf, DummyLiteral)) return null;

        var model = new List<(int, bool)>();

        model.AddRange(Storage[DummyLiteral]);
        Storage.Remove(DummyLiteral);

        foreach (var pair in Storage)
        {
            model.Add(pair.Key);
            model.AddRange(pair.Value);
        }

        if (model.Count != cnf.CountVars)
            model = InitModel(model, cnf.CountVars);

        return model.OrderBy(item => item.Item1).ToList();
    }

    private static List<(int, bool)> InitModel(List<(int, bool)> model, int literalsCount)
    {
        var arr = new (int, bool)[literalsCount];
        foreach (var literalValue in model)
        {
            if (literalValue.Item1 > 0)
            {
                arr[literalValue.Item1 - 1] = literalValue;
            }
            else
            {
                arr[-literalValue.Item1 - 1] = literalValue;
            }
        }

        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i].Item1 != 0)
                continue;

            arr[i] = (i + 1, true);
        }

        return arr.ToList();
    }

    private static bool DPLL(Cnf cnf, (int, bool) selectedLiteral)
    {
        Storage[selectedLiteral] = new List<(int, bool)>();

        while (cnf.UnitClause is { } unitClause)
        {
            var notAssigned = unitClause.NotAssigned;
            cnf = cnf.PropagateUnit(notAssigned);

            Storage[selectedLiteral].Add((notAssigned, notAssigned > 0));
        }


        while (true)
        {
            int PURELITERAL;
            PURELITERAL = cnf.PureLiteral;
            if (PURELITERAL == 0)
            {
                break;
            }

            cnf = cnf.EliminatePureLiteral(PURELITERAL);

            Storage[selectedLiteral].Add((PURELITERAL, PURELITERAL > 0));
            if (cnf.HasEmptyClause)
                return false;
        }

        if (cnf.HasEmptyClause)
            return false;
        if (cnf.IsEmpty)
            return true;

        var randomLiteral = cnf.GetLiteral();

        var isTrueBranch = DPLL(cnf.InsertValueToLiteral(randomLiteral, true), (randomLiteral, true));

        var isFalseBranch = false;
        if (!isTrueBranch)
        {
            isFalseBranch = DPLL(cnf.InsertValueToLiteral(randomLiteral, false), (randomLiteral, false));

            Storage.Remove((randomLiteral, true));
        }

        if (!isFalseBranch)
            Storage.Remove((randomLiteral, false));
        if (cnf.HasEmptyClause)
            return false;
        if (cnf.IsEmpty)
            return true;

        return isTrueBranch || isFalseBranch;
    }
}