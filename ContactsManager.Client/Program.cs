using ContactsManager.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ContactsManager.Client.Services;
using ContactsManager.Client.Services.IServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IContactService, ContactService>(client =>
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:BaseUrl"]!));

await builder.Build().RunAsync();
