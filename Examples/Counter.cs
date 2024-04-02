using static Examples.ClientBuilder;

namespace Examples;

public class Counter
{
    Client _client = SomeClient().WithAge(5).WithHeight(50).Build();
    
    public static int CountClumps(int[]? nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

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

class TestRunner
{
    private List<TestCase> testCases = new();

    void Run()
    {
        foreach (TestCase testCase in testCases)
        {
            testCase.Run(new TestResult());
        }
    }
}

class TestCase
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

class TestResult
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

class PaydayTransaction
{
    private readonly IPayRollRepository _payRollRepository;
    private readonly ITransactionRecorder _transactionRecorder;

    public PaydayTransaction(IPayRollRepository payRollRepository,
        ITransactionRecorder transactionRecorder)
    {
        _payRollRepository = payRollRepository;
        _transactionRecorder = transactionRecorder;
    }

    void Run()
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

class TransactionLog : ITransactionRecorder
{
    public void SaveTransaction(PaydayTransaction paydayTransaction)
    {
        throw new NotImplementedException();
    }
}

interface IPayRollRepository
{
}

class Account
{
    private int _balance;
    private readonly AcmeLogger _log;

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

class AcmeLogger
{
    public void LogTransaction(DateTime now, int i)
    {
        throw new NotImplementedException();
    }
}

class AccountAfterSubclassAndOverride
{
    private int _balance;
    private readonly AcmeLogger _log;

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

class AccountAfterSubclassAndOverrideForTesting : AccountAfterSubclassAndOverride
{
    public List<int> _loggedWithdrawals;
    public AccountAfterSubclassAndOverrideForTesting(int balance) : base(balance)
    {
        _loggedWithdrawals = new();
    }

    protected override void LogWithdraw(int value)
    {
        // do whatever we want, in this case spying  
        _loggedWithdrawals.Add(value);
    }
}

class AccountBeforeExtractMethod
{
    private int _balance;
    private readonly AcmeLogger _log;

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


class AccountDoingNastyThingsInConstructor 
{
    private readonly List<Transaction> _transactions;

    public AccountDoingNastyThingsInConstructor()
    {
        var transactionsRepository = new FileTransactionsRepository();
        _transactions = transactionsRepository.GetAll();
    }
}

class AccountAfterExtractAndOverrideFactoryMethod 
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

class ForTestingAccountAfterExtractAndOverrideFactoryMethod : AccountAfterExtractAndOverrideFactoryMethod
{
    private readonly List<Transaction> _transactions;

    public ForTestingAccountAfterExtractAndOverrideFactoryMethod(List<Transaction> transactions)
    {
        _transactions = transactions;
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
        List<Transaction> transactions = new List<Transaction>();
        
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
    private List<Item> _items;
    
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
    private List<Item> _items;

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
    private List<Item> _items;

    public RegisterSaleAfterSubclassAndOverride()
    {
        _items = new List<Item>();
    }

    public void AddItem(Barcode code)
    {
        var newItem = GetInventory().GetItemForBarCode(code);
        _items.Add(newItem);
    }

    protected virtual Inventory GetInventory()
    {
        return Inventory.GetInstance();
    }

    // more code...
}


public class Inventory
{
    private static Inventory? _instance;
    private Inventory()
    {
        // do somethings
    }

    public static Inventory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Inventory();
        }
        return _instance;
    }
    
    public Item GetItemForBarCode(Barcode code)
    {
       // getting the item somehow
       return new Item();
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
        if (_instance == null)
        {
            _instance = new ExternalRouter();
        }
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
        if (_instance == null)
        {
            _instance = new ExternalRouterAfterIntroducingSetter();
        }
        return _instance;
    }

    // Added for testing purposes only, do not use this in production code
    public static void SetInstanceForTesting(ExternalRouterAfterIntroducingSetter? instance)
    {
        _instance = instance;
    }
    
    // more code...
}


class BankingServices
{
    public static void UpdateAccountBalance(int userId, Money amount)
    {
        // some code to update the account balance
    }
    
    // more methods...
}

public class User
{
    private int _id;
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
    private int _id;
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
}

class ClientBuilder 
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

    public Client Build() {
        return new Client(_age, _height);
    }
}

// In some client
class SomeBuilderClient 
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

class Child 
{
    private int _age;
    
    public static Child MakeBaby()
    {
        return new Child(0);
    }

    private Child(int age) 
    {
        if (age >= 4)
        {
            throw new Exception("Not a child!");
        }
        _age = age;
    }

    int GetAge()
    {
        return _age;
    }
  
    void SetAge(int age) {
        _age = age;
    }
}