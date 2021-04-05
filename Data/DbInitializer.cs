using DISFinalProject.Data;
using DISFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DISFinalProject.Data
{
    public static class DbInitializer
    {
        static HttpClient httpClient;
        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "2tS2OFbpx613f4rFH61hm1KlduyObzOhyLVBXwBh"; 
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            getTopics(context);
            getActivities(context);
            getParks(context);
        }
        public static void getParks(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Parks.Any())
            {
                return;
            }

            string uri = BASE_URL + "/parks?limit=100";
            string responsebody = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!responsebody.Equals(""))
                {
                    JObject parsedResponse = JObject.Parse(responsebody);
                    JArray parks = (JArray)parsedResponse["data"];

                    foreach(JObject jsonpark in parks)
                    {
                        Park p = new Park 
                        {
                            ID = (string)jsonpark["id"],
                            url = (string)jsonpark["url"],
                            fullName = (string)jsonpark["fullName"],
                            parkCode = (string)jsonpark["parkCode"],
                            description = (string)jsonpark["description"],
                            states = (string)jsonpark["states"]
                        };
                        context.Parks.Add(p);
                        JArray activities = (JArray)jsonpark["activities"];
                        if(activities.Count != 0)
                        {
                            foreach (JObject jsonactivity in activities)
                            {
                                Activity a = context.Activities.Where(c => c.ID == (string)jsonactivity["id"]).FirstOrDefault();
                                if (a == null)
                                {
                                    a = new Activity
                                    {
                                        ID = (string)jsonactivity["id"],
                                        name = (string)jsonactivity["name"]
                                    };
                                    context.Activities.Add(a);
                                    context.SaveChanges();
                                }
                                ParkActivity pa = new ParkActivity
                                {
                                    activity = a,
                                    park = p
                                };
                                context.ParkActivities.Add(pa);
                            }
                        }
                        JArray topics = (JArray)jsonpark["topics"];
                        if(topics.Count != 0)
                        {
                            foreach (JObject jsontopic in topics)
                            {
                                Topic t = context.Topics.Where(c => c.ID == (string)jsontopic["id"]).FirstOrDefault();
                                if (t == null)
                                {
                                    t = new Topic
                                    {
                                        ID = (string)jsontopic["id"],
                                        name = (string)jsontopic["name"]
                                    };
                                    context.Topics.Add(t);
                                    context.SaveChanges();
                                }

                                ParkTopic pt = new ParkTopic
                                {
                                    topic = t,
                                    park = p
                                };
                                context.ParkTopics.Add(pt);
                            }
                            context.SaveChanges();
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void getTopics(ApplicationDbContext context)
        {
            if (context.Topics.Any())
            {
                return;
            }

            string uri = BASE_URL + "/topics?limit=100";
            string responsebody = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!responsebody.Equals(""))
                {
                    JObject parsedResponse = JObject.Parse(responsebody);
                    JArray topics = (JArray)parsedResponse["data"];
                    foreach(JObject jsontopic in topics)
                    {
                        Topic t = new Topic
                        {
                            ID = (string)jsontopic["id"],
                            name = (string)jsontopic["name"]
                        };
                        context.Topics.Add(t);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void getActivities(ApplicationDbContext context)
        {
            if (context.Activities.Any())
            {
                return;
            }

            string uri = BASE_URL + "/activities?limit=100";
            string responsebody = "";

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.BaseAddress = new Uri(uri);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!responsebody.Equals(""))
                {
                    JObject parsedResponse = JObject.Parse(responsebody);
                    JArray activities = (JArray)parsedResponse["data"];
                    foreach (JObject jsonactivity in activities)
                    {
                        Activity a = new Activity
                        {
                            ID = (string)jsonactivity["id"],
                            name = (string)jsonactivity["name"]
                        };
                        context.Activities.Add(a);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
