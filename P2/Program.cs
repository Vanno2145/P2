using System;
using System.Threading;

class Account
{
    private int balance;
    private readonly object lockObj = new object();

    public Account(int initialBalance)
    {
        balance = initialBalance;
    }

    public void Deposit(int amount)
    {
        lock (lockObj)
        {
            balance += amount;
            Console.WriteLine($"Пополнение счета на {amount}. Текущий баланс: {balance}.");
        }
    }

    public void Withdraw(int amount)
    {
        lock (lockObj)
        {
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Снятие со счета {amount}. Текущий баланс: {balance}.");
            }
            else
            {
                Console.WriteLine($"Не достаточно средств для снятия {amount}. Текущий баланс: {balance}.");
            }
        }
    }

    public int GetBalance()
    {
        lock (lockObj)
        {
            return balance;
        }
    }
}

class Client
{
    private static Random random = new Random();
    private Account account;

    public Client(Account account)
    {
        this.account = account;
    }

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(random.Next(1000, 3000));
            int amount = random.Next(1, 101);

            if (random.Next(2) == 0)
            {
                account.Deposit(amount);
            }
            else
            {
                account.Withdraw(amount);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Account account1 = new Account(1000);
        Account account2 = new Account(1000);
        Account account3 = new Account(1000);

        Client client1 = new Client(account1);
        Client client2 = new Client(account2);
        Client client3 = new Client(account3);

        Thread thread1 = new Thread(new ThreadStart(client1.Start));
        Thread thread2 = new Thread(new ThreadStart(client2.Start));
        Thread thread3 = new Thread(new ThreadStart(client3.Start));

        thread1.Start();
        thread2.Start();
        thread3.Start();

        Thread.Sleep(30000);

        Console.WriteLine("Программа завершена.");
        Console.WriteLine($"Состояние счета клиента 1: {account1.GetBalance()}.");
        Console.WriteLine($"Состояние счета клиента 2: {account2.GetBalance()}.");
        Console.WriteLine($"Состояние счета клиента 3: {account3.GetBalance()}.");
    }
}
