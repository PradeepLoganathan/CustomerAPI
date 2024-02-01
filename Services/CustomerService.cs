using Grpc.Core;
using CustomerAPI;
using static CustomerAPI.CustomerService;
using CustomerAPI.Data;

namespace CustomerAPI.Services;

public class CustomerService : CustomerServiceBase
{
    private readonly CustomerContext _context;

    public CustomerService(CustomerContext context)
    {
        _context = context;
    }

    public override async Task<GetCustomerByIdResponse> GetCustomerById(GetCustomerByIdRequest request, ServerCallContext context)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        if (customer == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Customer with ID={request.CustomerId} is not found."));
        }

        var response = new GetCustomerByIdResponse
        {
            Customer = new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            }
        };

        return response;
    }

    public override async Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest request, ServerCallContext context)
    {
        var customer = new Customer
        {
            Id = request.Customer.Id,
            FirstName = request.Customer.FirstName,
            LastName = request.Customer.LastName,
            Email = request.Customer.Email
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var response = new CreateCustomerResponse
        {
            Customer = request.Customer,
            Success = true,
            Message = "Customer created successfully"
        };

        return response;
    }

}
