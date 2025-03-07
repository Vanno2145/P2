using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int[] nums = new int[20];
        Task task1 = new Task(Sum);
        Task task2 = new Task(WNumbers);
        Task task3 = new Task(() => RNumbers(nums));

        task1.Start();
        task1.Wait();

        task2.Start();
        task2.Wait();

        task3.Start();
        task3.Wait();
    }

    static void Sum()
    {
        int sum = 0;
        for (int i = 0; i < 100; i++)
        {
            sum += i;
        }
    }

    public static void WNumbers()
    {

        int[] nums = new int[20];
        Random rand = new Random();
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = rand.Next(1, 101);
        }
        string path = "D:\\file.txt";
        using (StreamWriter file = new StreamWriter(path))
        {
            file.WriteLine(string.Join("", nums));
        }
    }

    public static void RNumbers(int[] nums)
    {
        string path = "D:\\file.txt";
        using (StreamReader sr = new StreamReader(path))
        {
            string fileContent = sr.ReadToEnd();
            nums = fileContent.Split(' ')
                        .Select(int.Parse)
                        .ToArray();
        }


        var binaryNumbers = nums.Select(num => Convert.ToString(num, 2)).ToArray();


        string path2 = "D:\\file2.txt";
        using (StreamWriter file = new StreamWriter(path2))
        {
            file.WriteLine(string.Join(" ", binaryNumbers));
        }

        foreach (var binary in binaryNumbers)
        {
            Console.WriteLine(binary);
        }
    }
}
