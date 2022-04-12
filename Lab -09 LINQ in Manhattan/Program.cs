using System;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;


namespace Lab__09_LINQ_in_Manhattan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Filter data = GetData();
                Neighborhoods(data);
                NeighborhoodsWithNoNames(data);
                RemoveDuplicates(data);
                ConsolidateIntoOneSingleQuery(data);
                LinqMethod(data);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine("Message: " + e.Message);
            }
        }
        public static void Neighborhoods(Filter filter) 
        {
            var q1 = from Feature in filter.features
                     select Feature.properties.neighborhood;
            int count = 1;
            Console.WriteLine("All neighborhoods");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); 
                count++;
            }

        }

        public static void NeighborhoodsWithNoNames(Filter filter)
        {
            var q1 = from Feature in filter.features
                     where Feature.properties.neighborhood != ""
                     select Feature.properties.neighborhood;
            int count = 1;
            Console.WriteLine("Filter out all the neighborhoods that do not have any names");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); 
                count++;
            }

        }
        public static void RemoveDuplicates(Filter filter)
        {
            var q1 = (from Feature in filter.features
                      where Feature.properties.neighborhood != ""
                      select Feature.properties.neighborhood).Distinct();
            int count = 1;
            Console.WriteLine("All neighborhoods without duplicates");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); 
                count++;
            }
        }
        public static void ConsolidateIntoOneSingleQuery(Filter filter)
        {
            var q1 = filter.features
                .Select(Feature => new { Feature.properties.neighborhood })
                .OrderByDescending(Feature => Feature.neighborhood)
                .Where(Feature => Feature.neighborhood != "");

            int count = 1;
            Console.WriteLine("Filter out all the neighborhoods that do not have any names");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}");
                count++;
            }
        }
        public static void LinqMethod(Filter filter)
        {
            var q1 = filter.features
                .Select(f => new { f.properties.neighborhood });
            int count = 1;
            Console.WriteLine("All neighborhoods");
            foreach (var element in q1)
            {
                Console.WriteLine($"{count}. {element}"); 
                count++;
            }
        }
        static Filter GetData()
        {
            
           // Filter filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText("C:\Users\HP\source\repos\Lab -09 LINQ in Manhattan\Lab -09 LINQ in Manhattan\Data.json"));
            Filter filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText("../../Data.json"));
            return filter;
        }
    }
}
