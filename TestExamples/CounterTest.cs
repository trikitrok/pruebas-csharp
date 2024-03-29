using Examples;

namespace TestExamples;

public class CounterTest
{
    [Test]
    [TestCase(new int[] { }, 0)] // empty
    [TestCase(null, 0)]
    [TestCase(new int[] { 1, 2, 2, 2 }, 1)] // one clump
    [TestCase(new int[] { 1 }, 0)] // one element
    [TestCase(new int[] { 1, 2, 2, 2, 3, 2, 2 }, 2)] // two clumps
    public void counting_clumps(int[] nums, int result)
    {
        Assert.That(Counter.CountClumps(nums), Is.EqualTo(result));
    }
}

public class RegisterSaleTest
{
    
    [Test] 
    public void counting_clumps()
    {
        Inventory inventory = NSubstitute.Substitute.For<Inventory>();
        RegisterSaleForTesting registerSale = new RegisterSaleForTesting(inventory);
        
        
    }
    public class RegisterSaleForTesting : RegisterSaleAfterSubclassAndOverride
    {
        private readonly Inventory _inventory;

        public RegisterSaleForTesting(Inventory inventory)
        {
            _inventory = inventory;
        }
        protected override Inventory GetInventory()
        {
            return _inventory;
        }
    }

}

public class MessageRouterTest
{
    [Test] 
    public void Routes_Message()
    {
        ExternalRouterAfterIntroducingSetter? externalRouter = NSubstitute.Substitute.For<ExternalRouterAfterIntroducingSetter>();
        ExternalRouterAfterIntroducingSetter.SetInstanceForTesting(externalRouter);
        var messageRouter = new MessageRouter();
        
        messageRouter.Route(new Message());
        
        // rest of the test...
    }

    [TearDown]
    public void TearDown()
    {
        ExternalRouterAfterIntroducingSetter.SetInstanceForTesting(null);
    }
    
}

public class BankingServiceClientTest
{
    [Test] 
    public void Updates_Balance()
    {
        BankingServices1 bankingServices = NSubstitute.Substitute.For<BankingServices1>();
        var user = new User1(1);
        
        user.UpdateBalance(new Money(200), bankingServices);
        
        // rest of the test...
    }
}

public class BankingServiceClientTest1
{
    [Test] 
    public void Updates_Balance()
    {
        BankingServices1 bankingServices = NSubstitute.Substitute.For<BankingServices1>();
        var user = new User2(1, bankingServices);
        
        user.UpdateBalance(new Money(200));
        
        // rest of the test...
    }
}

public class User2
{
    private readonly int _id;
    private readonly BankingServices1 _bankingServices;

    public User2(int id, BankingServices1 bankingServices)
    {
        _id = id;
        _bankingServices = bankingServices;
    }
    
    // more code...
    
    public void UpdateBalance(Money amount)
    {
        _bankingServices.UpdateBalance(_id, amount);
    }
    
    // more code...
}
