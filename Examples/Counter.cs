namespace Examples;

public class Counter
{
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