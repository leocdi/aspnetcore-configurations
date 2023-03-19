using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile(@"\\Win-s-19\silo\MyCustomSettings.json", optional: true, reloadOnChange: true);

using (var httpClient = new HttpClient())
{
    string conf = await httpClient.GetStringAsync("https://www.groseill.net/MyWebSettings.json");
    builder.Configuration.AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(conf)));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
