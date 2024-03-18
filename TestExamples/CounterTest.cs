using Examples;

namespace TestExamples;

public class CounterTest
{
    [Test]
    [TestCase(new int[] {}, 0)] // empty
    [TestCase(null, 0)]
    //[TestCase(new int[] {1, 2, 2, 2}, 1)] // one clump
    [TestCase(new int[] {1}, 0)] // one element
    public void counting_clumps(int[] nums, int result)
    {
        Assert.That(Counter.CountClumps(nums), Is.EqualTo(result));
    }
    
}