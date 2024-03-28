namespace ProblematicDiscount.Test;

public class DiscountTest
{

    [Test]
    public void Fix_Me()
    {
        var discount = new Discount();

        var net = new Money(1002);
        var total = discount.DiscountFor(net);

        Assert.That(new Money(901.8m), Is.EqualTo(total));
    }
    
    
}