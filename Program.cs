using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Context")));
// builder.Services.AddDbContext<Context>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString("Context")));
// builder.Services.AddDbContext<Context>(opt =>
//     opt.UseInMemoryDatabase("CardList"));

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "CardApi", Version = "v1" });
//});

// add builder for cors with any origins
builder.Services.AddCors(options =>
{
  options.AddPolicy(MyAllowSpecificOrigins,
  builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardApi v1"));
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();