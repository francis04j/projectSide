using Brokers;
using System;
using System.Linq;

namespace DatabaseConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using(var db = new StorageBroker())
            {
                db.Database.EnsureCreated();
                Console.WriteLine($"Database path: {db.DbPath}.");

                // Create
                Console.WriteLine("Inserting a new blog");
                var url = "http://blogs.msdn.com/adonet";
                db.Add(new Blog { Url = url});
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                Console.WriteLine($"same blog retrieved?: {blog.Url == url }");

            }
        }
    }
}
