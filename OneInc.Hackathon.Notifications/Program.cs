using Microsoft.EntityFrameworkCore;
using OneInc.Hackathon.SharedDatabase.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("OneInc.Hackathon.Notifications")));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
