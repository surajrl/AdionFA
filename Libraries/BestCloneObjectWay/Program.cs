
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var per1 = new Person { Type = nameof(Person), Name = "Pepe" };
        var per2 = (Person)per1.ShallowCopy();
        per2.Name = "Per2";

        Console.WriteLine(per2.Name);
        Console.WriteLine(per2.Name);

    }
}


public class ClassBase : ISerializable
{
    public string? Type { get; set; }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    public ClassBase ShallowCopy() 
    {
        return (ClassBase)MemberwiseClone();
    }
}

public class Person : ClassBase
{ 
    public string? Name { get; set; }
}

public static class Helper
{
    //public static T CreateDeepCopy<T>(T obj)
    //{
    //    using (var ms = new MemoryStream())
    //    {
    //        IFormatter formatter = new BinaryFormatter();
    //        formatter.Serialize(ms, obj);
    //        ms.Seek(0, SeekOrigin.Begin);
    //        return (T)formatter.Deserialize(ms);
    //    }
    //}
}




