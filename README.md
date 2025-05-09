
---

## ✅ Exemplo: Usando a API da OpenAI com C\#

### 🛠 Requisitos:

* .NET 6 ou 7 instalado
* Chave da API da OpenAI (você pode gerar em [https://platform.openai.com/](https://platform.openai.com/))
* Pacote `System.Net.Http.Json` (incluso no .NET 6+)

---

### 🧾 Passo 1 – Crie um novo projeto:

No terminal:

```bash
dotnet new console -n OpenAIChatExample
cd OpenAIChatExample
```

---

### 📦 Passo 2 – Cole o código abaixo no `Program.cs`:

```csharp
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
        string prompt = "Explique o que é programação orientada a objetos.";

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
```

---

### ▶️ Passo 3 – Execute o projeto:

```bash
dotnet run
```

---

### 🧠 Resultado esperado:

Você verá no console uma resposta gerada pelo ChatGPT explicando o conceito de programação orientada a objetos.

---

### ⚠️ Dica de segurança:

Nunca exponha sua chave da API em código público (como no GitHub). Use variáveis de ambiente ou arquivos `.env` em projetos reais.

---


