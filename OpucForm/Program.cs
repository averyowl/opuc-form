using OpucForm.Data;
using OpucForm.Models;
using OpucForm.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
// app.UseStaticFiles(); - Removed from .NET8
app.MapStaticAssets();
app.UseAntiforgery();

app.MapPost("/api/formentries", async (FormEntry entry, ApplicationDbContext db) =>
{
    entry.CreatedAt = DateTime.UtcNow;
    db.FormEntries.Add(entry);
    await db.SaveChangesAsync();
    return Results.Ok(entry);
});

app.MapGet("/api/formentries", async (ApplicationDbContext db) =>
    await db.FormEntries.OrderByDescending(f => f.Id).Take(50).ToListAsync());

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();

