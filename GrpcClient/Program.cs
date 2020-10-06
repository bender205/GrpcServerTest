using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string ADRESS = "https://localhost:5001";
            /*
                        var input = new HelloRequest {Name = "Skysoft"};

                        var channel = GrpcChannel.ForAddress(ADRESS);
                        var  client = new Greeter.GreeterClient(channel);

                        var reply = await client.SayHelloAsync(input);

                        Console.WriteLine(reply.Message);*/

            using var channel = GrpcChannel.ForAddress(ADRESS);
            var client = new Customer.CustomerClient(channel);

            var input = new CustomerLookupModel { UserId = 1 };
            var reply = await client.GetCustomerInfoAsync(input);

            Console.WriteLine(reply.FirstName);
            Console.WriteLine(reply.LastName);

            Console.WriteLine("new customers calling");

            using var call = client.GetNewCustomers(new NewCustomerRequest());
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.EmailAddress} {currentCustomer.Age}");
            }


            Console.ReadLine();
        }
    }
}
