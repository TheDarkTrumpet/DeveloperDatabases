using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMicroORM.Models;
using PetaPoco;

namespace CSharpMicroORM
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PetaPoco.Database("Data Source=localhost;Initial Catalog=AdventureWorks;Integrated Security=True", "System.Data.SqlClient");

            var records = db.Query<Person>("select FirstName,MiddleName,LastName,Demographics,Suffix from Person.Person");
            var record = records.First();

            Console.WriteLine("Suffix: " + record.Suffix);
            Console.WriteLine("FName: " + record.FirstName);
            Console.WriteLine("Middle: " + record.MiddleName);
            Console.WriteLine("LName: " + record.LastName);
            Console.WriteLine("Demographics: " + record.Demographics);


            Console.ReadLine();
        }
    }
}
