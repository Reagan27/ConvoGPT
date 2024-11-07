using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvoGPT_console.GPT
{
    public class GptService
    {
        class GPTRoles
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        private string Name { get; set; }
        public async Task sendRequest(string name, string ActAsA)
        {
            while (true)
            {

                Name = name;
                List<GPTRoles> gPTRoles = new List<GPTRoles>();
                Console.WriteLine("Ask A question");
                Console.WriteLine($" [{Name}]:");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                var betterPrompt = " I need you to act as an " + ActAsA + "And in all answers always start with my name which is " + name;
                gPTRoles.Add(new GPTRoles() { role = "system", content = betterPrompt });
                gPTRoles.Add(new GPTRoles() { role = "user", content = input });

                var API_KEY = "";//;
                var _url = "https://api.openai.com/v1/chat/completions";

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

                var body = new
                {
                    model = "gpt-3.5-turbo",
                    messages = gPTRoles
                };

                //serialize 

                var bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(_url, bodyContent);

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();

                    var res = JObject.Parse(responseBody);
                    var content = res["choices"][0]["message"]["content"].ToString();
                    Console.WriteLine(content);
                    gPTRoles.Add(new GPTRoles() { role = "assistant", content = content });
                }
                else
                {
                    Console.WriteLine("Api Request Failed");
                }
            }
        }
    }
}