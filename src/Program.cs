using CommandsApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

//public IConfiguration Configuration { get; } esto ya no funciona
Console.WriteLine("punto de control 1.0");
var builder = WebApplication.CreateBuilder(args);
//Construir la cadena de conexión con los datos de secrets.json




if (builder.Environment.IsDevelopment())
{
    var strBuilder = new SqlConnectionStringBuilder();
    strBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    Console.WriteLine("punto de control 1.1");
    strBuilder.UserID = builder.Configuration["userId"];
    strBuilder.Password = builder.Configuration["password"];
    Console.WriteLine("punto de control 2");
    Console.WriteLine("punto de control isdevelopment() -> true");
    builder.Services.AddDbContext<ComandoContext>(opt => opt.UseSqlServer(strBuilder.ConnectionString));
}
else
{
    Console.WriteLine("punto de control isdevelopment() -> false");
    var strBuilder = new NpgsqlConnectionStringBuilder();
    strBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    Console.WriteLine("punto de control 1.1");
    strBuilder.Username = builder.Configuration["userId"];
    strBuilder.Password = builder.Configuration["password"];
    Console.WriteLine("punto de control 2");
    builder.Services.AddDbContext<ComandoContext>(opt => opt.UseNpgsql(strBuilder.ConnectionString));
}
Console.WriteLine("punto de control 3");
builder.Services.AddControllers();
//builder.Services.AddScoped<ICommandsAPIRepo, MockCommandAPIRepo>(); //La implementación falsa
builder.Services.AddScoped<ICommandsAPIRepo, ComandoEnSql>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson(s => new CamelCasePropertyNamesContractResolver());

Console.WriteLine("punto de control 4");
var app = builder.Build();

//var context = app.Services.GetRequiredService<ComandoEnSql>(); No sé por qué no se puede hacer así
//context.Database.Migrate();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ComandoContext>();
    context.Database.Migrate();
}
Console.WriteLine("punto de control 5");
app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");
//app.UseEndpoints(endpoints => endpoints.MapControllers()); esto ya no funciona
app.MapControllers();
Console.WriteLine("punto de control 6");
app.Run();

