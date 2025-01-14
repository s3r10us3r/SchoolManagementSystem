using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SchoolManagementSystem.Shared.Services.Extensions;
using SchoolManagementSystem.Shared.WebServices.Extensions;
using SchoolManagementSystem.WebClient;
using SchoolManagementSystem.WebClient.Services.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSharedServices();
builder.Services.AddWebServices();
builder.Services.AddServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:7191") });

await builder.Build().RunAsync();
