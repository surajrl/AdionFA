using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Security
{
    class Program
    {
        static void Main(string[] args)
        {
            new Test();

            // Create generic identity.
            GenericIdentity myIdentity = new GenericIdentity("MyIdentity");
            // Create generic principal.
            String[] myStringArray = { "Manager", "Teller" };
            GenericPrincipal myPrincipal = new GenericPrincipal(myIdentity, myStringArray);

            // Attach the principal to the current thread.
            // This is not required unless repeated validation must occur,
            // other code in your application must validate, or the
            // PrincipalPermission object is used.
            //Thread.CurrentPrincipal = myPrincipal;

            // Print values to the console.
            String name = myPrincipal.Identity.Name;
            bool auth = myPrincipal.Identity.IsAuthenticated;
            bool isInRole = myPrincipal.IsInRole("Manager");

            Console.WriteLine("The name is: {0}", name);
            Console.WriteLine("The isAuthenticated is: {0}", auth);
            Console.WriteLine("Is this a Manager? {0}", isInRole);
        }
    }


    [AttributeTest("")]
    public class Test
    { 
    }

    public class AttributeTest : Attribute
    {
        public AttributeTest(string role)
        {
            throw new Exception("test");
        }

        public string Role { get; set; }
    }
}
