using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiDbContext;
using MultiDbContext.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=WriteDb"));

//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ReadDb"));

// builder.Services.AddDbContext<MyDbContext>(options =>
// {
//     options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=WriteDb",
//         npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
//
//     options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ReadDb",
//         npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
// });

//var readOptionsBuilder = new DbContextOptionsBuilder<MyDbContext>()
//    .UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=WriteDb");

//var writeOptionsBuilder = new DbContextOptionsBuilder<MyDbContext>()
//    .UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ReadDb");
//builder.Services.AddScoped<MyDbContext>(_ => new MyDbContext(readOptionsBuilder.Options));

// builder.Services.AddScoped<MyDbContext>(provider =>
// {
//     var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
//     optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ReadDb");
//     return new MyDbContext(optionsBuilder.Options);
// });

//builder.Services.AddDbContext<MyDbContext>("Database1", options =>
//    options.UseSqlServer("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ReadDb")
//);

// builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("Database1")));
//
// builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("Database2")));
//
builder.Services.AddScoped<IProductService>(serviceProvider =>
{
    var readDbContext = serviceProvider.GetRequiredService<MyDbContext>();
    var writeDbContext = serviceProvider.GetRequiredService<MyDbContext>();

    //return new ProductService(writeDbContext, readDbContext);
});



builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WriteDb")));
builder.Services.AddScoped<IProductService, ProductService>();

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

app.Run();