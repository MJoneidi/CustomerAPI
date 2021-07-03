using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TestSimulator
{
    public enum TaskType
    {
        Get,
        Post
    }
    public class InternalTask
    {
        public TaskType TaskType { get; set; }
        public List<CustomerRequest> Requests { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Simulator !");
            Thread.Sleep(10000);

            var tasks = CreateTask();

            CancellationTokenSource cancel = new CancellationTokenSource();
            cancel.CancelAfter(7500);
            Parallel.ForEach(tasks, new ParallelOptions() { CancellationToken = cancel.Token }, (task, obj, index) =>
            {
                Task.Factory.StartNew(() =>
                {
                    if(task.TaskType == TaskType.Get)
                    {
                        var getResult = GetMethod();
                    }
                    else
                    {
                        var postResult = PostMethod(task.Requests);
                    }    
                });
            });
            
            Console.ReadLine();
        }



        static bool PostMethod(List<CustomerRequest> request)
        {
            var url = new Uri($"https://localhost:44363/");
            
            string json = JsonConvert.SerializeObject(request, Formatting.None);
            HttpClient client = new HttpClient { BaseAddress = url };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("Customer", content).Result;
            return response.IsSuccessStatusCode;
        }

        static List<CustomerRequest> GetMethod()
        {
            var url = new Uri($"https://localhost:44363/");
            var client = new HttpClient { BaseAddress = url };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =  client.GetAsync("Customer").Result;
            if(response.IsSuccessStatusCode)
            {
                var result =  response.Content.ReadAsStringAsync().Result;
                client.Dispose();
                return JsonConvert.DeserializeObject<List<CustomerRequest>>(result);                
            }
            return null;
        }

        private static List<InternalTask> CreateTask()
        {
            List<InternalTask> tasks = new List<InternalTask>();
            Random random = new Random();


            tasks.Add(new InternalTask() { TaskType = TaskType.Get });
            tasks.Add(new InternalTask() { 
                TaskType = TaskType.Post, 
                Requests= new List<CustomerRequest>() 
                { 
                    new CustomerRequest() { Age= 56, FirstName= "Aaaa", LastName= "Aaaa", ID=2 },
                    new CustomerRequest() { Age= 32, FirstName= "Aaaa", LastName= "Cccc", ID=5 },
                    new CustomerRequest() { Age= 50, FirstName= "Bbbb", LastName= "Cccc", ID=1 },
                    new CustomerRequest() { Age= 70, FirstName= "Aaaa", LastName= "Dddd", ID=4 }
                } 
            });
            tasks.Add(new InternalTask() { TaskType = TaskType.Get });
            tasks.Add(new InternalTask()
            {
                TaskType = TaskType.Post,
                Requests = new List<CustomerRequest>()
                {
                    new CustomerRequest() { Age= 26, FirstName= "Bbbb", LastName= "Bbbb", ID=6 },
                    new CustomerRequest() { Age= 32, FirstName= "Aaaa", LastName= "Bbbb", ID=7 }                   
                }
            });
            tasks.Add(new InternalTask() { TaskType = TaskType.Get });
            tasks.Add(new InternalTask()
            {
                TaskType = TaskType.Post,
                Requests = new List<CustomerRequest>()
                {
                    new CustomerRequest() { Age= 16, FirstName= "Bbbb", LastName= "ffff", ID=6 },
                    new CustomerRequest() { Age= 90, FirstName= "Aaaa", LastName= "Bjklb", ID=70 }
                }
            });
            tasks.Add(new InternalTask() { TaskType = TaskType.Get });
            tasks.Add(new InternalTask()
            {
                TaskType = TaskType.Post,
                Requests = new List<CustomerRequest>()
                {
                    new CustomerRequest() { Age= 36, FirstName= "Bbbb", LastName= "ffff", ID=16 },
                    new CustomerRequest() { Age= 90, FirstName= "Aaaa", LastName= "ffff", ID=70 }
                }
            });
            tasks.Add(new InternalTask() { TaskType = TaskType.Get });
            return tasks;
        }
    }
}
