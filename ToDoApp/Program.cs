using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoApp.Data;
using ToDoApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseInMemoryDatabase("ToDoList"));

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

var app = builder.Build();

// Seed the database with sample data
SeedDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ToDoContext>();

        if (!context.ToDoItems.Any())
        {
            context.ToDoItems.AddRange(
                new ToDoItem { Name = "Add new item", IsComplete = false },
                new ToDoItem { Name = "Finish reports", IsComplete = true },
                new ToDoItem { Name = "Call her", IsComplete = false }
            );

            context.SaveChanges();
        }
    }
}
