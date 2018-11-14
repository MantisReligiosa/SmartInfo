using Nancy.Hosting.Self;
using System;
using Web;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var hostConfiguration = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var uri = new Uri("http://localhost:1234");
            var bootstrapper = new Bootstrapper();
            var nancyHost = new NancyHost(bootstrapper, hostConfiguration, uri);

            //var nancyHost = new NancyHost(new Uri("http://localhost:1234"), new Bootstrapper());
            nancyHost.Start();
            Console.WriteLine("Service started!");
            Console.ReadLine();
            nancyHost.Stop();
            Console.WriteLine("Service stoped!");
        }
    }
}
