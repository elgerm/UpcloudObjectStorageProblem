using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var upcloud = new UpcloudS3();

            Console.WriteLine("Listing buckets... this works:");
            await upcloud.ListBuckets();

            Console.WriteLine();
            Console.WriteLine("Getting an item from a bucket... this doesnt work:");
            await upcloud.GetObject();

            Console.ReadLine();
        }
    }
}
