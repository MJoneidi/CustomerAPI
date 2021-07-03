using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace TestSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Simulator !");
            Thread.Sleep(2000);
            GetMethod();
            Console.ReadLine();
        }

        static void PostMethod(List<CustomerRequest> request)
        {
            var url = new Uri($"https://localhost:44363/");
            
            string json = JsonConvert.SerializeObject(request, Formatting.None);
            HttpClient client = new HttpClient { BaseAddress = url };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("Customer", content).Result;

        }

        static void GetMethod()
        {
            var url = new Uri($"https://localhost:44363/");
            var client = new HttpClient { BaseAddress = url };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =  client.GetAsync("Customer").Result;
            if(response.IsSuccessStatusCode)
            {
                var result =  response.Content.ReadAsStringAsync().Result;
                client.Dispose();
                var xx= JsonConvert.DeserializeObject<List<CustomerRequest>>(result);
                Console.WriteLine($"{xx.Count}");
            }
            
        }
    }
}
