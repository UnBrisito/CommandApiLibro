using CommandsApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//public IConfiguration Configuration { get; } esto ya no funciona

var builder = WebApplication.CreateBuilder(args);
//Construir la cadena de conexión con los datos de secrets.json
var strBuilder = new SqlConnectionStringBuilder();
strBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
strBuilder.UserID = builder.Configuration["userId"];
strBuilder.Password = builder.Configuration["password"];


builder.Services.AddDbContext<ComandoContext>(opt => opt.UseSqlServer(strBuilder.ConnectionString));
builder.Services.AddControllers();
//builder.Services.AddScoped<ICommandsAPIRepo, MockCommandAPIRepo>(); //La implementación falsa
builder.Services.AddScoped<ICommandsAPIRepo, ComandoEnSql>();

var app = builder.Build();

app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");
//app.UseEndpoints(endpoints => endpoints.MapControllers()); esto ya no funciona
app.MapControllers();
app.Run();