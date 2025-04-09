using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string imageUrl = "https://via.placeholder.com/300";
        string localPath = Path.Combine(Directory.GetCurrentDirectory(), "downloaded_image.jpg");

        using HttpClient client = new();

        try
        {
            byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
            await File.WriteAllBytesAsync(localPath, imageBytes);
            Console.WriteLine($"Изображение успешно сохранено: {localPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при загрузке изображения: " + ex.Message);
        }
    }
}
