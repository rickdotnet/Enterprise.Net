namespace Enterprise.Tests;

public class TddTests
{
    [Fact]
    public void We_Are_Not_Down_With_Tdd()
    {
        var tdd = new Tdd();
        
        // drinking out of cups, not eva
        Assert.False(tdd.YouDown());
    }
}