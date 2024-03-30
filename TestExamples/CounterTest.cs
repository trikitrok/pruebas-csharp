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

public class RSCWorkflow
{
    private static int MAX_LENGTH = 200;
    
    //... more code
    
    public static void Validate(Packet packet)
    {
        if (packet.GetOriginator() == "MIA" 
            || packet.GetLength() > MAX_LENGTH
            || !packet.HasValidCheckSum())
        {
            throw new InvalidFlowException();
        }
        //... more code that does not use instance data or methods    
    }
    
    //... more code
}

public class InvalidFlowException : Exception
{
}

public class Packet
{
    public string GetOriginator()
    {
        throw new NotImplementedException();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    public bool HasValidCheckSum()
    {
        throw new NotImplementedException();
    }
}

public class GDIBrush
{
    private int _colorId;
    
    // A long method
    public void Draw(List<Point> renderingRoots,
        ColorMatrix colors,
        List<Point> selection)
    {
        // some more code in the method
        foreach (var point in renderingRoots)
        {
            // a lot more code in the loop
            DrawPoint(point.X, point.Y, colors.GetColor(_colorId));
        }
        
        // a lot more code in the method
    }

    private void DrawPoint(int x, int y, Color color)
    {
        
    }
}

public interface PointRenderer
{
    int ColorId { get; }
    void DrawPoint(int x, int y, Color color);
}

public class GDIBrush1 : PointRenderer
{
    public int ColorId { get; }

    // A long method
    public void Draw(List<Point> renderingRoots,
        ColorMatrix colors,
        List<Point> selection)
    {
        new Renderer(this, renderingRoots, colors, selection).Draw();
    }

    public void DrawPoint(int x, int y, Color color)
    {
        
    }
}

public class Renderer
{
    private readonly PointRenderer _pointRenderer;
    private readonly List<Point> _renderingRoots;
    private readonly ColorMatrix _colors;
    private readonly List<Point> _selection;

    public Renderer(PointRenderer pointRenderer, List<Point> 
        renderingRoots, 
        ColorMatrix colors, 
        List<Point> selection)
    {
        _pointRenderer = pointRenderer;
        _renderingRoots = renderingRoots;
        _colors = colors;
        _selection = selection;
        throw new NotImplementedException();
    }

    public void Draw()
    {
        // some more code in the method
        foreach (var point in _renderingRoots)
        {
            // a lot more code in the loop
            _pointRenderer.DrawPoint(point.X, point.Y, _colors.GetColor(_pointRenderer.ColorId));
        }
        
        // a lot more code in the method
    }
}

public class Color
{
}

public class ColorMatrix
{
    public Color GetColor(int colorId)
    {
        throw new NotImplementedException();
    }
}

public class Point
{
    public int Y { get; set; }
    public int X { get; }
}