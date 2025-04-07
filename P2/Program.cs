using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

class Program
{
    
    public static string GlobalText = "Это пример глобального текста";

    static async Task Main(string[] args)
    {
        Console.WriteLine("==== Обработка текста через скрипт ====\n");
        Console.WriteLine("Глобальный текст:\n" + GlobalText + "\n");

        Console.WriteLine("Примеры команд:");
        Console.WriteLine("  GlobalText.Length            // Кол-во символов");
        Console.WriteLine("  GlobalText.Split(' ').Length // Кол-во слов");
        Console.WriteLine("Введите выражение для выполнения или 'exit':\n");

        while (true)
        {
            Console.Write(">> ");
            string code = Console.ReadLine();

            if (code?.ToLower() == "exit") break;

            try
            {
                var result = await CSharpScript.EvaluateAsync(
                    code,
                    ScriptOptions.Default
                        .AddReferences(typeof(Program).Assembly)
                        .AddImports("System"),
                    globals: new Globals());

                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Ошибка: {ex.Message}");
            }
        }
    }

    
    public class Globals
    {
        public string GlobalText => Program.GlobalText;
    }
}
