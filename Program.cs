using Grpc.Reflection;

using CustomerAPI.Services;
using CustomerAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseInMemoryDatabase("CustomerDb"));
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CustomerService>();
app.MapGrpcReflectionService();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
