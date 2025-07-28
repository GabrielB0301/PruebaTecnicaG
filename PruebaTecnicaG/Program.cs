using PruebaTecnicaG.Components;
using System.Net.Http.Headers;
using PruebaTecnicaG.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://mainserver.ziursoftware.com/")
});


builder.Services.AddScoped<ApiService>();
builder.Services.AddHttpClient();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
