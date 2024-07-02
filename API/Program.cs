using Core.Interfaces;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

// add classes for DI like below
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Generic classes sathi you have to do like following
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
ConfigurationManager configuration = builder.Configuration;

//Start: MiddleWare configuration section
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
// .net core app runs inside a process and you can check which process like below if u select kestrel server.
//app.MapGet("/", () => $"Worker Process Name : {System.Diagnostics.Process.GetCurrentProcess().ProcessName}"); 
//End:MiddleWare configuration section

//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//var context = services.GetRequiredService<StoreContext>();
//var logger = services.GetRequiredService<ILogger<Program>>();

//try
//{
//    await context.Database.MigrateAsync();
//}
//catch (Exception ex)
//{

//    logger.LogError(ex, "An Error occurreed during migration");
//}

app.Run();
