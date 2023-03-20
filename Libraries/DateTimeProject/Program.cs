using DateTimeProject.Enums;
using DateTimeProject.Extensions;
using System;

namespace DateTimeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = new DateTime(2022, 05, 15);
            //var ts = new TimeSpan(0, 12, 0, 0);
            //
            //dt = dt.Add(ts);
            //
            //Console.WriteLine(dt);
            //
            //var dt2 = DateTime.Parse("2019-01-17T12:00:00");
            //
            //Console.WriteLine(dt2);
            //Console.WriteLine(dt >= dt2);

            //----------------------------

            Console.WriteLine(DateTime.Today.DiffTimeInUoM(dt, (int)UoMEnum.Day));
        }

    }
}
