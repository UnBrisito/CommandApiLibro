using CommandsApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandsAPIRepo, MockCommandAPIRepo>();

var app = builder.Build();

app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello World!");
//app.UseEndpoints(endpoints => endpoints.MapControllers()); esto ya no funciona
app.MapControllers();
app.Run();