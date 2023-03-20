using Newtonsoft.Json;
using System;
using System.IO;

namespace Protobuf
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person 
            { 
                FirstName = "Adniers",
                Age = 36,
            };

            File.WriteAllText("person.json", JsonConvert.SerializeObject(person));
        }
    }


    public class Person
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}
