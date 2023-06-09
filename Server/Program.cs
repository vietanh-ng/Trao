using TraoApp.Server.Hubs;
using TraoApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR(option =>
{
    option.DisableImplicitFromServicesParameters = true;
});

builder.Services.AddSingleton<SensorService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<SensorService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseWebSockets();
app.MapHub<SensorHub>("/hub/sensor");

app.Run();
