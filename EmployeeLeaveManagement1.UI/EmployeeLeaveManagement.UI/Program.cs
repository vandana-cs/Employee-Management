using EmployeeLeaveManagement.UI.Authentication;
using EmployeeLeaveManagement.UI.Components;
using EmployeeLeaveManagement.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Connect Blazor UI to API
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5008/")
    });

// Register services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LeaveTypeService>();
builder.Services.AddScoped<LeaveRequestService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Keep this commented while using HTTP
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();