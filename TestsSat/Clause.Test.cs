using SAT;

namespace TestsSat;

public class Clause_Test
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        var clause = new Clause(new List<int>() { 1, 2, 3 });
        Assert.That(clause.ToString(), Is.EqualTo("(1 \\/ 2 \\/ 3)"));
    }
    
    [Test]
    public void Test2()
    {
        var clause = new Clause(new List<int>() { 1 });
        Assert.That(clause.IsUnitClause, Is.True);
    }
    
    [Test]
    public void Test3()
    {
        var clause = new Clause(new List<int>() {  });
        Assert.That(clause.IsEmpty, Is.True);
    }

}