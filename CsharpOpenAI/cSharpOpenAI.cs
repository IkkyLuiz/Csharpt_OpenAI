using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string apiKey = "SUA_CHAVE_DA_OPENAI"; // Substitua pela sua chave da OpenAI
        string prompt = "Explique o que é POO - Programação Orientada a Objetos.";

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        string responseJson = await response.Content.ReadAsStringAsync();

        using JsonDocument json = JsonDocument.Parse(responseJson);
        string reply = json.RootElement
                           .GetProperty("choices")[0]
                           .GetProperty("message")
                           .GetProperty("content")
                           .GetString();

        Console.WriteLine("Resposta da IA:");
        Console.WriteLine(reply);
    }
}
