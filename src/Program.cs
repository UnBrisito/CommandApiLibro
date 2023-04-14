using CommandsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//public IConfiguration Configuration { get; } esto ya no funciona

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ComandoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
builder.Services.AddControllers();
//builder.Services.AddScoped<ICommandsAPIRepo, MockCommandAPIRepo>();
builder.Services.AddScoped<ICommandsAPIRepo, ComandoEnSql>();

var app = builder.Build();

app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");
//app.UseEndpoints(endpoints => endpoints.MapControllers()); esto ya no funciona
app.MapControllers();
app.Run();