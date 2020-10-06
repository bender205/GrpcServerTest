using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "1";
                output.LastName = "1";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "2";
                output.LastName = "2";
            }
            else 
            {
                output.FirstName = "else";
                output.LastName = "else";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "1",
                    LastName = "1",
                    Age = 11,
                    EmailAddress = "1@mail.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "2",
                    LastName = "2",
                    Age = 22,
                    EmailAddress = "2@mail.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "3",
                    LastName = "3",
                    Age = 33,
                    EmailAddress = "3@mail.com",
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
