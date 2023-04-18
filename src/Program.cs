using CommandsApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Hosting;

//public IConfiguration Configuration { get; } esto ya no funciona

var builder = WebApplication.CreateBuilder(args);
//Construir la cadena de conexión con los datos de secrets.json
var strBuilder = new SqlConnectionStringBuilder();

strBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
strBuilder.UserID = builder.Configuration["userId"];
strBuilder.Password = builder.Configuration["password"];

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ComandoContext>(opt => opt.UseSqlServer(strBuilder.ConnectionString));
}
else
{
    builder.Services.AddDbContext<ComandoContext>(opt => opt.UseNpgsql(strBuilder.ConnectionString));
}
builder.Services.AddControllers();
//builder.Services.AddScoped<ICommandsAPIRepo, MockCommandAPIRepo>(); //La implementación falsa
builder.Services.AddScoped<ICommandsAPIRepo, ComandoEnSql>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson(s => new CamelCasePropertyNamesContractResolver());


var app = builder.Build();

//var context = app.Services.GetRequiredService<ComandoEnSql>(); No sé por qué no se puede hacer así
//context.Database.Migrate();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ComandoContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");
//app.UseEndpoints(endpoints => endpoints.MapControllers()); esto ya no funciona
app.MapControllers();

app.Run();

