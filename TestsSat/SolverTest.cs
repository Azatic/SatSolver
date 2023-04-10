using SAT;

namespace TestsSat;

public class Tests
{
    private Parser _parser;
    private readonly string _cnfSatyes = string.Join('\n',
        "c SAT",
        "p cnf 8 5",
        "4 -5 3 0",
        "-5 -2 -4 0",
        "8 -1 2 0",
        "-3 5 6 0",
        "2 1 8 0");
    
    private readonly string _cnfSatno = string.Join('\n',
        "c SAT",
        "p cnf 2 4",
        "1 2 0",
        "-1 -2 0",
        "1 -2 0",
        "-1 2 0");
    
    private readonly string _cnf_Sat_with_one_answer = string.Join('\n',
        "c SAT",
        "p cnf 2 3",
        "1 2",
        "-1 -2",
        "1 -2"
        );
    [SetUp]
    public void Setup()
    {
        _parser = new();
    }

    [Test]
    public void Test1()
    {
        var cnfSat = _parser.ParseText(_cnfSatyes); 
        var someModel = Solver.SolveSat(cnfSat);
        var isSat = someModel != null;
        Assert.That(isSat, Is.True);
    }
    
    [Test]
    public void Test2()
    {
        var cnfSat = _parser.ParseText(_cnfSatno); 
        var someModel = Solver.SolveSat(cnfSat);
        var isSat = someModel != null;
        Assert.That(isSat, Is.False);
    }
    [Test]
    public void Test3()
    {
        var cnfSat = _parser.ParseText(_cnf_Sat_with_one_answer); 
        var someModel = Solver.SolveSat(cnfSat);
        var isSat = someModel;
        Assert.That(someModel, Is.EqualTo(new List<(int, bool)>{ (-2,false),(1, true)}));
    }
}