using SAT;

namespace TestsSat;

public class CnfTest
{
        Clause _clause1 = new Clause(new List<int>() { 1, 2, 3 });
        Clause _clause2 = new Clause(new List<int>() { 2, 3});
        [SetUp]
    public void Setup()
    {
    
    }

    [Test]
    public void Test1()
    {
        Cnf _testcnf1 = new Cnf(new List<Clause>(){_clause1,_clause2});
        Assert.That(1,Is.EqualTo(_testcnf1.PureLiteral));
    }
    [Test]
    public void Test2()
    {
        Cnf _testcnf1 = new Cnf(new List<Clause>(){_clause1,_clause2});
        Assert.That(1,Is.EqualTo(_testcnf1.PureLiteral));
    }
    
    [Test]
    public void Test3()
    {
        Cnf _testcnf1 = new Cnf(new List<Clause>(){_clause1,_clause2});
        Cnf _testcnf2 = new Cnf(new List<Clause>() { new Clause(new List<int>() { 2, 3 }) });
        Assert.That(_testcnf2.ToString(),Is.EqualTo(_testcnf1.InsertValueToLiteral(1,true).ToString()));
    }

}