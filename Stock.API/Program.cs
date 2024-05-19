using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Stock.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("StockDb");
});
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        //dockerize edilseydi burdaki adres dockerize adresi olacakti
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        //subcribe olacak



    });

});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//using kullanýlýyor çünkü memeoryden düþmesini bekliyoruz. aksi takdirde scope memoryde kalýr
using (var scope=app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Stocks.Add(new Stock.API.Models.Stock()
    {
        Id=1,
        ProductId=1,
        Count=100

    });
    context.Stocks.Add(new Stock.API.Models.Stock()
    {
        Id = 2,
        ProductId = 2,
        Count = 100

    });
    context.SaveChanges();
}

app.Run();
