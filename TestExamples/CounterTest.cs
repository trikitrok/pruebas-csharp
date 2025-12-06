using Examples;
using NSubstitute;

namespace TestExamples;

public class CounterTest
{
    [TestCase(new int[] { }, 0)] // empty
    [TestCase(null, 0)]
    [TestCase(new[] { 1, 2, 2, 2 }, 1)] // one clump
    [TestCase(new[] { 1 }, 0)] // one element
    [TestCase(new[] { 1, 2, 2, 2, 3, 2, 2 }, 2)] // two clumps
    public void counting_clumps(int[] nums, int result)
    {
        Assert.That(Counter.CountClumps(nums), Is.EqualTo(result));
    }
}

public class PersonNameTest
{
    [Test]
    public void Should_Be_Represented_As_List_Name()
    {
        AssertListName(
            new PersonName("Fran", "Iglesias Gómez"),
            "Iglesias Gómez, Fran"
        );
    }

    [Test]
    public void Should_Be_Represented_As_Full_Name()
    {
        AssertFullName(
            new PersonName("Fran", "Iglesias Gómez"),
            "Fran Iglesias Gómez"
        );
    }

    [Test]
    public void Should_Be_Represented_As_DNI()
    {
        AssertDniName(
            new PersonName("Francisco José", "Iglesias Gómez"),
            "IGLESIAS<GOMEZ<<FRANCISCO<JOSE"
        );
    }

    [Test]
    public void Should_Be_Represented_As_DTO()
    {
        AssertDto(
            new PersonName("Fran", "Iglesias Gómez"),
            new PersonNameDto("Fran", "Iglesias Gómez")
        );
    }

    public void AssertDto(PersonName name, PersonNameDto expected)
    {
        var representation = new DBPersonNameRepresentation();
        name.Fill(representation);
        var dto = representation.Dto();
        Assert.That(dto, Is.EqualTo(expected));
    }

    private void AssertListName(PersonName name, string expected)
    {
        var representation = new ListPersonNameRepresentation();
        name.Fill(representation);
        Assert.That(representation.Serialize(), Is.EqualTo(expected));
    }

    private void AssertFullName(PersonName name, string expected)
    {
        var representation = new FullPersonNameRepresentation();
        name.Fill(representation);
        Assert.That(representation.Serialize(), Is.EqualTo(expected));
    }

    private void AssertDniName(PersonName name, string expected)
    {
        var representation = new DniPersonNameRepresentation();
        name.Fill(representation);
        Assert.That(representation.Serialize(), Is.EqualTo(expected));
    }
}

public class RegisterSaleTest
{
    [Test]
    public void counting_clumps()
    {
        var inventory = Substitute.For<IInventory>();
        var registerSale = new RegisterSaleForTesting(inventory);
    }

    public class RegisterSaleForTesting : RegisterSaleAfterSubclassAndOverride
    {
        private readonly IInventory _inventory;

        public RegisterSaleForTesting(IInventory inventory)
        {
            _inventory = inventory;
        }

        protected override IInventory GetInventory()
        {
            return _inventory;
        }
    }
}

public class MessageRouterTest
{
    [Test]
    [Ignore("Ignore test")]
    public void Routes_Message()
    {
        var externalRouter =
            Substitute.For<ExternalRouterAfterIntroducingSetter>();
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
        var bankingServices = Substitute.For<BankingServices1>();
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
        var bankingServices = Substitute.For<BankingServices1>();
        var user = new User2(1, bankingServices);

        user.UpdateBalance(new Money(200));

        // rest of the test...
    }
}

public class User2
{
    private readonly BankingServices1 _bankingServices;
    private readonly int _id;

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
    private static readonly int MAX_LENGTH = 200;

    //... more code

    public static void Validate(Packet packet)
    {
        if (packet.GetOriginator() == "MIA"
            || packet.GetLength() > MAX_LENGTH
            || !packet.HasValidCheckSum())
            throw new InvalidFlowException();
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
            // a lot more code in the loop
            DrawPoint(point.X, point.Y, colors.GetColor(_colorId));

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

    public void DrawPoint(int x, int y, Color color)
    {
    }

    // A long method
    public void Draw(List<Point> renderingRoots,
        ColorMatrix colors,
        List<Point> selection)
    {
        new Renderer(this, renderingRoots, colors, selection).Draw();
    }
}

public class Renderer
{
    private readonly ColorMatrix _colors;
    private readonly PointRenderer _pointRenderer;
    private readonly List<Point> _renderingRoots;
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
            // a lot more code in the loop
            _pointRenderer.DrawPoint(point.X, point.Y, _colors.GetColor(_pointRenderer.ColorId));

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

public class IntegratedCompanyTest
{
    private IntegratedCompany _company;
    private CompanyApi _companyApi;
    private OpeningListener _openingListener;

    [SetUp]
    public void Init()
    {
        _companyApi = Substitute.For<CompanyApi>();
        _openingListener = Substitute.For<OpeningListener>();
        _company = new IntegratedCompany(_companyApi, _openingListener);
    }

    [Test]
    public void Successfully_Opening_A_Claim()
    {
        var refenceInCompany = "reference in company";
        var claimId = "some id";
        var companyId = "AcmeCompany";
        var claim = new Claim(claimId, companyId);
        var openedClaimData = new OpenedClaimData(refenceInCompany, claimId);
        _companyApi.Open(claim)
            .Returns(OpeningResult.Success(openedClaimData._refenceInCompany, openedClaimData._claimId));

        _company.Open(claim);

        _openingListener.Received().OpeningSucceeded(new OpeningSuccess(refenceInCompany, claimId, companyId));
    }

    [Test]
    public void Fails_Opening_A_Claim()
    {
        var claimId = "some id";
        var companyId = "AcmeCompany";
        var claim = new Claim(claimId, companyId);
        var description = "ooh falló";
        _companyApi.Open(claim).Returns(OpeningResult.Failure(claimId, description));

        _company.Open(claim);

        _openingListener.Received().OpeningFailed(new OpeningFailure(description, claimId));
    }
}

public class OpeningFailure
{
    private readonly string _claimId;
    private readonly string? _description;

    public OpeningFailure(string? description, string claimId)
    {
        _description = description;
        _claimId = claimId;
    }

    public override string ToString()
    {
        return $"{nameof(_description)}: {_description}, {nameof(_claimId)}: {_claimId}";
    }

    protected bool Equals(OpeningFailure other)
    {
        return _description == other._description && _claimId == other._claimId;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OpeningFailure)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_description, _claimId);
    }
}

public class OpeningSuccess
{
    private readonly string _claimId;
    private readonly string _companyId;
    private readonly string? _refenceInCompany;

    public OpeningSuccess(string? refenceInCompany, string claimId, string companyId)
    {
        _refenceInCompany = refenceInCompany;
        _claimId = claimId;
        _companyId = companyId;
    }

    public override string ToString()
    {
        return
            $"{nameof(_refenceInCompany)}: {_refenceInCompany}, {nameof(_claimId)}: {_claimId}, {nameof(_companyId)}: {_companyId}";
    }

    protected bool Equals(OpeningSuccess other)
    {
        return _refenceInCompany == other._refenceInCompany && _claimId == other._claimId &&
               _companyId == other._companyId;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OpeningSuccess)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_refenceInCompany, _claimId, _companyId);
    }
}

public class OpenedClaimData
{
    public readonly string _claimId;
    public readonly string? _refenceInCompany;

    public OpenedClaimData(string? refenceInCompany, string claimId)
    {
        _refenceInCompany = refenceInCompany;
        _claimId = claimId;
    }
}

public class OpeningResult
{
    private readonly string _claimId;
    private readonly string? _failureDescription;
    private readonly OpenedClaimData _openedClaimData;
    private readonly string? _referenceInCompany;

    private OpeningResult(string? refenceInCompany, string claimId, string? failureDescription)
    {
        _openedClaimData = new OpenedClaimData(refenceInCompany, claimId);
        _failureDescription = failureDescription;
        _claimId = claimId;
        _referenceInCompany = refenceInCompany;
    }

    public static OpeningResult Success(string refenceInCompany, string claimId)
    {
        return new OpeningResult(refenceInCompany, claimId, null);
    }

    public static OpeningResult Failure(string claimId, string description)
    {
        return new OpeningResult(null, claimId, description);
    }

    public void Notify(string claimCompanyId, OpeningListener openingListener)
    {
        if (IsFailure())
            openingListener.OpeningFailed(
                new OpeningFailure(_failureDescription, _openedClaimData._claimId)
            );
        else
            openingListener.OpeningSucceeded(
                new OpeningSuccess(_referenceInCompany, _claimId, claimCompanyId)
            );
    }

    private bool IsFailure()
    {
        return _referenceInCompany == null;
    }
}

public class IntegratedCompany
{
    private readonly CompanyApi _companyApi;
    private readonly OpeningListener _openingListener;

    public IntegratedCompany(CompanyApi companyApi, OpeningListener openingListener)
    {
        _companyApi = companyApi;
        _openingListener = openingListener;
    }

    public void Open(Claim claim)
    {
        var result = _companyApi.Open(claim);
        result.Notify(claim._companyId, _openingListener);
    }

    public void Open1(Claim claim)
    {
        var result = _companyApi.Open1(claim);
        result.Notify(claim._companyId, _openingListener);
    }
}

public interface OpeningListener
{
    void OpeningSucceeded(OpeningSuccess openingSuccess);
    void OpeningFailed(OpeningFailure openingFailure);
}

public interface CompanyApi
{
    OpeningResult Open(Claim claim);
    OpeningResult2 Open1(Claim claim);
}

public class Claim
{
    private readonly string _claimId;
    public readonly string _companyId;

    public Claim(string claimId, string companyId)
    {
        _claimId = claimId;
        _companyId = companyId;
    }
}

public class IntegratedCompany2Test
{
    private IntegratedCompany _company;
    private CompanyApi _companyApi;
    private OpeningListener _openingListener;

    [SetUp]
    public void Init()
    {
        _companyApi = Substitute.For<CompanyApi>();
        _openingListener = Substitute.For<OpeningListener>();
        _company = new IntegratedCompany(_companyApi, _openingListener);
    }

    [Test]
    public void Successfully_Opening_A_Claim()
    {
        var refenceInCompany = "reference in company";
        var claimId = "some id";
        var companyId = "AcmeCompany";
        var claim = new Claim(claimId, companyId);
        _companyApi.Open1(claim).Returns(OpeningResult2.Success(refenceInCompany, claimId));

        _company.Open1(claim);

        _openingListener.Received().OpeningSucceeded(new OpeningSuccess(refenceInCompany, claimId, companyId));
    }

    [Test]
    public void Fails_Opening_A_Claim()
    {
        var claimId = "some id";
        var companyId = "AcmeCompany";
        var claim = new Claim(claimId, companyId);
        var description = "ooh falló";
        _companyApi.Open1(claim).Returns(OpeningResult2.Failure(claimId, description));

        _company.Open1(claim);

        _openingListener.Received().OpeningFailed(new OpeningFailure(description, claimId));
    }
}

public abstract class OpeningResult2
{
    public static OpeningResult2 Success(string referenceInCompany, string claimId)
    {
        return new SuccessfulOpeningResult(claimId, referenceInCompany);
    }

    public static OpeningResult2 Failure(string claimId, string description)
    {
        return new FailingOpeningResult(claimId, description);
    }

    public abstract void Notify(string claimCompanyId, OpeningListener openingListener);

    private class SuccessfulOpeningResult(string claimId, string refenceInCompany) : OpeningResult2
    {
        public override void Notify(string claimCompanyId, OpeningListener openingListener)
        {
            openingListener.OpeningSucceeded(
                new OpeningSuccess(
                    refenceInCompany,
                    claimId,
                    claimCompanyId)
            );
        }
    }

    private class FailingOpeningResult(string claimId, string failureDescription) : OpeningResult2
    {
        public override void Notify(string claimCompanyId, OpeningListener openingListener)
        {
            openingListener.OpeningFailed(
                new OpeningFailure(
                    failureDescription, claimId)
            );
        }
    }
}