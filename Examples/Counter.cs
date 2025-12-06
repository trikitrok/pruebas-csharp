using System.Globalization;
using System.Text;
using static Examples.ClientBuilder;

namespace Examples;

public class Counter
{
    private Client _client = SomeClient().WithAge(5).WithHeight(50).Build();

    public static int CountClumps(int[]? nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        var count = 0;
        var prev = nums[0];
        var inClump = false;

        for (var i = 1; i < nums.Length; i++)
        {
            if (nums[i] == prev && !inClump)
            {
                inClump = true;
                count += 1;
            }

            if (nums[i] != prev)
            {
                prev = nums[i];
                inClump = false;
            }
        }

        var testCase = new TestCase();
        testCase.Run(new TestResult());

        return count;
    }
}

internal class TestRunner
{
    private readonly List<TestCase> testCases = new();

    private void Run()
    {
        foreach (var testCase in testCases) testCase.Run(new TestResult());
    }
}

internal class TestCase
{
    public void Run(TestResult result)
    {
        try
        {
            SetUp();
            RunTest(result);
        }
        catch (Exception e)
        {
            result.AddFailure(e, this);
        }

        TearDown();
    }

    private void TearDown()
    {
    }

    private void RunTest(TestResult result)
    {
        result.WasRun();
    }

    private void SetUp()
    {
    }
}

internal class TestResult
{
    private bool _wasRun;

    public TestResult()
    {
        _wasRun = false;
    }

    public void AddFailure(Exception exception, TestCase testCase)
    {
    }

    public void WasRun()
    {
        _wasRun = true;
    }
}

internal class PaydayTransaction
{
    private readonly IPayRollRepository _payRollRepository;
    private readonly ITransactionRecorder _transactionRecorder;

    public PaydayTransaction(IPayRollRepository payRollRepository,
        ITransactionRecorder transactionRecorder)
    {
        _payRollRepository = payRollRepository;
        _transactionRecorder = transactionRecorder;
    }

    private void Run()
    {
        // does important stuff, like paying the employees :)
        // ...

        _transactionRecorder.SaveTransaction(this);
    }
}

internal interface ITransactionRecorder
{
    void SaveTransaction(PaydayTransaction paydayTransaction);
}

internal class TransactionLog : ITransactionRecorder
{
    public void SaveTransaction(PaydayTransaction paydayTransaction)
    {
        throw new NotImplementedException();
    }
}

internal interface IPayRollRepository
{
}

internal class Account
{
    private readonly AcmeLogger _log;
    private int _balance;

    public Account(int balance)
    {
        _balance = balance;
        _log = new AcmeLogger();
    }

    public void Withdraw(int value)
    {
        _balance += value;
        LogWithdraw(value);
    }

    private void LogWithdraw(int value)
    {
        _log.LogTransaction(DateTime.Now, -value);
    }
}

internal class AcmeLogger
{
    public void LogTransaction(DateTime now, int i)
    {
        throw new NotImplementedException();
    }
}

internal class AccountAfterSubclassAndOverride
{
    private readonly AcmeLogger _log;
    private int _balance;

    public AccountAfterSubclassAndOverride(int balance)
    {
        _balance = balance;
        _log = new AcmeLogger();
    }

    public void Withdraw(int value)
    {
        _balance += value;
        LogWithdraw(value);
    }

    protected virtual void LogWithdraw(int value)
    {
        _log.LogTransaction(DateTime.Now, -value);
    }
}

internal class AccountAfterSubclassAndOverrideForTesting : AccountAfterSubclassAndOverride
{
    public List<int> _loggedWithdrawals;

    public AccountAfterSubclassAndOverrideForTesting(int balance) : base(balance)
    {
        _loggedWithdrawals = new List<int>();
    }

    protected override void LogWithdraw(int value)
    {
        // do whatever we want, in this case spying  
        _loggedWithdrawals.Add(value);
    }
}

internal class AccountBeforeExtractMethod
{
    private readonly AcmeLogger _log;
    private int _balance;

    public AccountBeforeExtractMethod(int balance)
    {
        _balance = balance;
        _log = new AcmeLogger();
    }

    public void Withdraw(int value)
    {
        _balance += value;
        _log.LogTransaction(DateTime.Now, -value);
    }
}

internal class AccountDoingNastyThingsInConstructor
{
    private readonly List<Transaction> _transactions;

    public AccountDoingNastyThingsInConstructor()
    {
        var transactionsRepository = new FileTransactionsRepository();
        _transactions = transactionsRepository.GetAll();
    }
}

internal class AccountAfterExtractAndOverrideFactoryMethod
{
    private List<Transaction> _transactions;

    public AccountAfterExtractAndOverrideFactoryMethod()
    {
        _transactions = GetAllTransactions();
    }

    protected virtual List<Transaction> GetAllTransactions()
    {
        return new FileTransactionsRepository().GetAll();
    }
}

internal class ForTestingAccountAfterExtractAndOverrideFactoryMethod : AccountAfterExtractAndOverrideFactoryMethod
{
    private static List<Transaction> _transactions;

    private ForTestingAccountAfterExtractAndOverrideFactoryMethod()
    {
    }

    public static AccountAfterExtractAndOverrideFactoryMethod Create(List<Transaction> someTransactions)
    {
        // The field transactions and its initialization need to be static 
        // so that the field gets initialized before the super class constructor is called
        _transactions = someTransactions;
        return new ForTestingAccountAfterExtractAndOverrideFactoryMethod();
    }

    protected override List<Transaction> GetAllTransactions()
    {
        return _transactions;
    }
}

internal class FileTransactionsRepository : ITransactionsRepository
{
    public List<Transaction> GetAll()
    {
        var transactions = new List<Transaction>();

        // read the transactions from some file...

        return transactions;
    }
}

internal interface ITransactionsRepository
{
    List<Transaction> GetAll();
}

internal class Transaction
{
}

public class RegisterSale
{
    private readonly List<Item> _items;

    public RegisterSale()
    {
        _items = new List<Item>();
    }

    public void AddItem(Barcode code)
    {
        var newItem = Inventory.GetInstance().GetItemForBarCode(code);
        _items.Add(newItem);
    }

    // more code...
}

public class RegisterSaleAfterExtractMethod
{
    private readonly List<Item> _items;

    public RegisterSaleAfterExtractMethod()
    {
        _items = new List<Item>();
    }

    public void AddItem(Barcode code)
    {
        var newItem = GetInventory().GetItemForBarCode(code);
        _items.Add(newItem);
    }

    private static Inventory GetInventory()
    {
        return Inventory.GetInstance();
    }

    // more code...
}

public class RegisterSaleAfterSubclassAndOverride
{
    private readonly List<Item> _items;

    public RegisterSaleAfterSubclassAndOverride()
    {
        _items = new List<Item>();
    }

    public void AddItem(Barcode code)
    {
        var newItem = GetInventory().GetItemForBarCode(code);
        _items.Add(newItem);
    }

    protected virtual IInventory GetInventory()
    {
        return Inventory.GetInstance();
    }

    // more code...
}

public interface IInventory
{
    Item GetItemForBarCode(Barcode code);
}

public class Inventory : IInventory
{
    private static Inventory? _instance;

    private Inventory()
    {
        // do something
    }

    public Item GetItemForBarCode(Barcode code)
    {
        // getting the item somehow
        return new Item();
    }

    public static Inventory GetInstance()
    {
        if (_instance == null) _instance = new Inventory();

        return _instance;
    }
}

public class Barcode
{
}

public class Item
{
}

public class MessageRouter
{
    public void Route(Message message)
    {
        ExternalRouter.GetInstance().sendMessage(message);
    }
}

public class ExternalRouter
{
    private static ExternalRouter? _instance;

    private ExternalRouter()
    {
        // initialize stuff
    }

    public static ExternalRouter GetInstance()
    {
        if (_instance == null) _instance = new ExternalRouter();

        return _instance;
    }

    // more code...
    public void sendMessage(Message message)
    {
        // interesting code to send the message
    }
}

public class Message
{
}

public class ExternalRouterAfterIntroducingSetter
{
    private static ExternalRouterAfterIntroducingSetter? _instance;

    private ExternalRouterAfterIntroducingSetter()
    {
        // initialize stuff
    }

    public static ExternalRouterAfterIntroducingSetter GetInstance()
    {
        if (_instance == null) _instance = new ExternalRouterAfterIntroducingSetter();

        return _instance;
    }

    // Added for testing purposes only, do not use this in production code
    public static void SetInstanceForTesting(ExternalRouterAfterIntroducingSetter? instance)
    {
        _instance = instance;
    }

    // more code...
}

internal class BankingServices
{
    public static void UpdateAccountBalance(int userId, Money amount)
    {
        // some code to update the account balance
    }

    // more methods...
}

public class User
{
    private readonly int _id;

    public User(int id)
    {
        _id = id;
    }

    // more code...

    public void UpdateBalance(Money amount)
    {
        BankingServices.UpdateAccountBalance(_id, amount);
    }

    // more code...
}

public class BankingServicesAlternative
{
    public static void UpdateAccountBalance(int userId, Money amount)
    {
        new BankingServicesAlternative().UpdateBalance();
    }

    public void UpdateBalance()
    {
        // some code to update the account balance
    }

    // more methods...
}

public class BankingServices1
{
    public static void UpdateAccountBalance(int userId, Money amount)
    {
        // some code to update the account balance
    }

    public void UpdateBalance(int userId, Money amount)
    {
        UpdateAccountBalance(userId, amount);
    }

    // more methods...
}

public class User1
{
    private readonly int _id;

    public User1(int id)
    {
        _id = id;
    }

    // more code...

    public void UpdateBalance(Money amount, BankingServices1 bankingServices)
    {
        bankingServices.UpdateBalance(_id, amount);
    }

    // more code...
}

public class Money
{
    private readonly int _amount;

    public Money(int amount)
    {
        _amount = amount;
    }

    public void Add(Money money)
    {
        throw new NotImplementedException();
    }
}

internal class ClientBuilder
{
    private int _age;
    private float _height;

    private ClientBuilder()
    {
    }

    public static ClientBuilder SomeClient()
    {
        return new ClientBuilder();
    }

    public ClientBuilder WithAge(int age)
    {
        _age = age;
        return this;
    }


    public ClientBuilder WithHeight(float height)
    {
        _height = height;
        return this;
    }

    public Client Build()
    {
        return new Client(_age, _height);
    }
}

// In some client
internal class SomeBuilderClient
{
    public void SomeMethod()
    {
        SomeClient().WithAge(5).WithHeight(50).Build();
        // a.b().c().d() 
        // Is this a Message Chain smell?
    }
}

internal class Client
{
    private int _age;
    private float _height;

    public Client(int age, float height)
    {
        _age = age;
        _height = height;
    }
}

internal class Child
{
    private int _age;

    private Child(int age)
    {
        if (age >= 4) throw new Exception("Not a child!");

        _age = age;
    }

    public static Child MakeBaby()
    {
        return new Child(0);
    }

    private int GetAge()
    {
        return _age;
    }

    private void SetAge(int age)
    {
        _age = age;
    }
}

public class Library
{
    private readonly StreamWriter _writer;

    public Library(StreamWriter writer)
    {
        _writer = writer;
    }

    public void PrintBooks(List<Book> books)
    {
        using (var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
        {
            var libraryName = reader.ReadLine();
            _writer.WriteLine(libraryName);
            foreach (var book in books) _writer.WriteLine(book.GetName());
        }
    }
}

public class Book
{
    private readonly string _name;

    public Book(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}

public class LibraryAfter
{
    private readonly StreamWriter _writer;

    public LibraryAfter(StreamWriter writer)
    {
        _writer = writer;
    }

    public void PrintBooks(List<Book> books, StreamReader streamReader)
    {
        using var reader = streamReader;
        var libraryName = reader.ReadLine();
        _writer.WriteLine(libraryName);
        foreach (var book in books) _writer.WriteLine(book.GetName());
    }
}

public class LibraryAdaptParameter
{
    private readonly StreamWriter _writer;

    public LibraryAdaptParameter(StreamWriter writer)
    {
        _writer = writer;
    }

    public void PrintBooks(List<Book> books, StreamReader streamReader)
    {
        using var reader = streamReader;
        var libraryName = reader.ReadLine();
        _writer.WriteLine(libraryName);
        foreach (var book in books) _writer.WriteLine(book.GetName());
    }
}

public class LibraryAdaptParameter2
{
    private readonly StreamWriter _writer;

    public LibraryAdaptParameter2(StreamWriter writer)
    {
        _writer = writer;
    }

    public void PrintBooks(List<Book> books, LibraryData libraryData)
    {
        var libraryName = libraryData.GetLibraryName();
        _writer.WriteLine(libraryName);
        foreach (var book in books) _writer.WriteLine(book.GetName());
    }
}

public interface LibraryData
{
    string? GetLibraryName();
}

public class StreamReaderLibraryData : LibraryData
{
    private readonly StreamReader _streamReader;

    public StreamReaderLibraryData(StreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public string? GetLibraryName()
    {
        using var reader = _streamReader;
        var libraryName = reader.ReadLine();
        return libraryName;
    }
}

internal class Reservation
{
    private readonly Customer _customer;
    private int _dailyRate;
    private readonly DateTime _date;
    private int _duration;
    private readonly List<FeeRider> _fees;

    public Reservation(Customer customer, int duration,
        int dailyRate, DateTime date)
    {
        _fees = new List<FeeRider>();
        _customer = customer;
        _duration = duration;
        _dailyRate = dailyRate;
        _date = date;
    }

    public void Extend(int additionalDays)
    {
        _duration += additionalDays;
    }

    public void ExtendForWeek()
    {
        var weekRemainder = RentalCalendar.WeekRemainderFor(_date);
        const int DAYS_PER_WEEK = 7;
        Extend(weekRemainder);
        _dailyRate = RateCalculator.ComputeWeekly(
                         _customer.GetRateCode())
                     / DAYS_PER_WEEK;
    }

    public void AddFee(FeeRider rider)
    {
        _fees.Add(rider);
    }

    public int GetTotalFee()
    {
        return GetPrincipalFee() + GetAdditionalFees();
    }

    private int GetAdditionalFees()
    {
        var total = 0;
        foreach (var fee in _fees) total += fee.GetAmount();

        return total;
    }

    private int GetPrincipalFee()
    {
        return _dailyRate
               * RateCalculator.RateBase(_customer)
               * _duration;
    }
}

internal class FeeRider
{
    public int GetAmount()
    {
        throw new NotImplementedException();
    }
}

internal class RateCalculator
{
    public static int ComputeWeekly(object getRateCode)
    {
        throw new NotImplementedException();
    }

    public static int RateBase(Customer customer)
    {
        throw new NotImplementedException();
    }
}

internal class RentalCalendar
{
    public static int WeekRemainderFor(DateTime date)
    {
        throw new NotImplementedException();
    }
}

internal class Fee
{
}

internal class Customer
{
    public object GetRateCode()
    {
        throw new NotImplementedException();
    }
}

public class CppClass
{
    private readonly List<Declaration> _declarations;
    private readonly string _name;

    public CppClass(string name, List<Declaration> declarations)
    {
        _name = name;
        _declarations = declarations;
    }

    public int GetDeclarationCount()
    {
        return _declarations.Count();
    }

    public string GetName()
    {
        return _name;
    }

    public Declaration GetDeclaration(int index)
    {
        return _declarations[index];
    }

    public string GetInterface(string interfaceName, List<int> indices)
    {
        var result = "class " + interfaceName + " {\n + public:\n";

        foreach (var index in indices)
        {
            var virtualFunction = _declarations[index];
            result += "\t" + virtualFunction.AsAbstract() + "\n";
        }

        result += "};\n";
        return result;
    }
}

public class Declaration
{
    public string AsAbstract()
    {
        throw new NotImplementedException();
    }
}

public class ClassReader
{
    private readonly List<Declaration> _declarations;
    private bool _inPublicSection;
    private CppClass? _parsedClass;
    private readonly Reader _reader;

    public ClassReader(Reader reader)
    {
        _reader = reader;
        _declarations = new List<Declaration>();
        _inPublicSection = false;
        _parsedClass = null;
    }

    public void Parse()
    {
        var source = new TokenReader(_reader);
        var classToken = source.readToken();
        var className = source.readToken();
        var lbrace = source.readToken();
        matchBody(source);
        var rbrace = source.readToken();
        var semicolon = source.readToken();

        if (classToken.getType() == Token.CLASS
            && className.getType() == Token.IDENT
            && lbrace.getType() == Token.LBRACE
            && rbrace.getType() == Token.RBRACE
            && semicolon.getType() == Token.SEMIC)
            _parsedClass = new CppClass(className.getText(),
                _declarations);
    }

    private void matchBody(TokenReader source)
    {
        // ...
    }

    // ...
}

public class Token
{
    public static string CLASS;
    public static string IDENT;
    public static string LBRACE;
    public static string RBRACE;
    public static string SEMIC;

    public string getType()
    {
        throw new NotImplementedException();
    }

    public string getText()
    {
        throw new NotImplementedException();
    }
}

public class Reader
{
}

internal class TokenReader
{
    private readonly Reader _reader;

    public TokenReader(Reader reader)
    {
        _reader = reader;
    }

    public Token readToken()
    {
        throw new NotImplementedException();
    }
}

public class InMemoryDirectory
{
    private readonly List<Element> _elements;

    public InMemoryDirectory()
    {
        _elements = new List<Element>();
    }

    public void AddElement(Element newElement)
    {
        _elements.Add(newElement);
    }

    public void GenerateIndex()
    {
        var index = new Element("index");
        foreach (var element in _elements) index.AddText(element.GetName() + "\n");
        AddElement(index);
    }

    public int GetElementCount()
    {
        return _elements.Count();
    }

    public Element? GetElement(string name)
    {
        foreach (var element in _elements)
            if (element.GetName().Equals(name))
                return element;

        return null;
    }
}

public class Element
{
    private readonly string _name;
    private string _text;

    public Element(string name)
    {
        _name = name;
        _text = "";
    }

    public void AddText(string newText)
    {
        _text += newText;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}

public class Invoice
{
    private OurDate _billingDate;
    private OurDate _openingDate;
    private Originator _originator;

    ///...
    public Money GetValue()
    {
        var total = ItemsSum();
        if (_billingDate.After(OurDate.YearEnd(_openingDate)))
        {
            if (_originator.GetState().Equals("FL") ||
                _originator.GetState().Equals("NY"))
                total.Add(GetLocalShipping());
            else
                total.Add(GetDefaultShipping());
        }
        else
        {
            total.Add(GetSpanningShipping());
        }

        total.Add(GetTax());
        return total;
    }

    private Money GetTax()
    {
        throw new NotImplementedException();
    }

    private Money GetSpanningShipping()
    {
        throw new NotImplementedException();
    }

    private Money GetDefaultShipping()
    {
        throw new NotImplementedException();
    }

    private Money GetLocalShipping()
    {
        throw new NotImplementedException();
    }

    private Money ItemsSum()
    {
        throw new NotImplementedException();
    }
    //...
}

public class Originator
{
    public string GetState()
    {
        throw new NotImplementedException();
    }
}

public class OurDate
{
    public bool After(OurDate date)
    {
        throw new NotImplementedException();
    }

    public static OurDate YearEnd(object date)
    {
        throw new NotImplementedException();
    }
}

public class Invoice1
{
    private readonly ShippingPricer _shippingPricer;

    public Invoice1(OurDate billingDate, OurDate openingDate, Originator originator)
    {
        /// ...
        _shippingPricer = new ShippingPricer(billingDate, openingDate, originator);
    }

    ///...
    public Money GetValue()
    {
        var total = ItemsSum();
        total.Add(_shippingPricer.GetPrice());
        total.Add(GetTax());
        return total;
    }

    private Money GetTax()
    {
        throw new NotImplementedException();
    }

    private Money ItemsSum()
    {
        throw new NotImplementedException();
    }
    //...
}

public class ShippingPricer
{
    private readonly OurDate _billingDate;
    private readonly OurDate _openingDate;
    private readonly Originator _originator;

    public ShippingPricer(OurDate billingDate, OurDate openingDate, Originator originator)
    {
        _billingDate = billingDate;
        _openingDate = openingDate;
        _originator = originator;
    }

    public Money GetPrice()
    {
        if (_billingDate.After(OurDate.YearEnd(_openingDate)))
        {
            if (_originator.GetState().Equals("FL") ||
                _originator.GetState().Equals("NY"))
                return GetLocalShipping();

            return GetDefaultShipping();
        }

        return GetSpanningShipping();
    }

    private Money GetSpanningShipping()
    {
        throw new NotImplementedException();
    }

    private Money GetDefaultShipping()
    {
        throw new NotImplementedException();
    }

    private Money GetLocalShipping()
    {
        throw new NotImplementedException();
    }
}

public sealed class PersonName
{
    private readonly string _name;
    private readonly string _surname;

    public PersonName(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }

    public void Fill(PersonNameRepresentation representation)
    {
        representation.SetName(_name);
        representation.SetSurname(_surname);
    }
}

public interface PersonNameRepresentation
{
    void SetName(string name);
    void SetSurname(string surname);
}

public class ListPersonNameRepresentation : PersonNameRepresentation
{
    private string _name;
    private string _surname;

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetSurname(string surname)
    {
        _surname = surname;
    }

    public string Serialize()
    {
        return $"{_surname}, {_name}";
    }
}

public class FullPersonNameRepresentation : PersonNameRepresentation
{
    private string _name;
    private string _surname;

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetSurname(string surname)
    {
        _surname = surname;
    }

    public string Serialize()
    {
        return $"{_name} {_surname}";
    }
}

public class DniPersonNameRepresentation : PersonNameRepresentation
{
    private string _name;
    private string _surname;

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetSurname(string surname)
    {
        _surname = surname;
    }

    public string Serialize()
    {
        var fullName = string.Format("{1}<<{0}", RemovePunctuation(_name).Trim(), RemovePunctuation(_surname).Trim())
            .Replace(" ", "<")
            .ToUpperInvariant();
        return fullName;
    }

    private static string RemovePunctuation(string text)
    {
        return new string(
                text.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray()
            )
            .Normalize(NormalizationForm.FormC);
    }
}

public class DBPersonNameRepresentation : PersonNameRepresentation
{
    private string _name;
    private string _surname;

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetSurname(string surname)
    {
        _surname = surname;
    }

    public PersonNameDto Dto()
    {
        return new PersonNameDto(_name, _surname);
    }
}

public class PersonNameDto
{
    private readonly string _name;
    private readonly string _surname;

    public PersonNameDto(string name, string surname)
    {
        _name = name;
        _surname = surname;
    }

    public string Name()
    {
        return _name;
    }

    public string Surname()
    {
        return _surname;
    }

    protected bool Equals(PersonNameDto other)
    {
        return _name == other._name && _surname == other._surname;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PersonNameDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _surname);
    }
}