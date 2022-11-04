using Elsa;
using Elsa.Activities.Signaling.Services;
using Elsa.Activities.Temporal;
using Elsa.Activities.UserTask.Extensions;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using Elsa.Workflows.CustomActivities.Signals.Bookmark;
using ElsaQuickstarts.Server.DashboardAndServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var elsaSection = builder.Configuration.GetSection("Elsa");

// Elsa services.
builder.Services
    .AddElsa(elsa => elsa
        .UseEntityFrameworkPersistence(ef => ef.UseSqlite())
        .AddConsoleActivities()
        .AddHttpActivities(elsaSection.GetSection("Server").Bind)
        .AddQuartzTemporalActivities()
        .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
        .AddUserTaskActivities()
        .AddCommonTemporalActivities()
        .AddWorkflowsFrom<Program>()
        .AddActivitiesFrom<Program>()
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          //policy.WithOrigins("http://localhost:4200", "https://localhost:4200");
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddTransient<ICustomSignaler, CustomSignaler>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<FileReferred>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<FileReceived>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<FCTitleOrdered>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<FCSCRAEligibilityReview>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<TitleReportReceived>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<PreliminaryTitleClear>>();
builder.Services.AddBookmarkProvider<VIABookmarkProvider<ComplaintFiled>>();

//builder.Services.AddBookmarkProvider<FileReceivedBookmarkProvider>();
//builder.Services.AddBookmarkProvider<TitleOrderedBookmarkProvider>();

// Elsa API endpoints.
builder.Services.AddElsaApiEndpoints();

// For Dashboard.
builder.Services.AddRazorPages();

builder.Services.AddSingleton(CrudClient.Instance);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(MyAllowSpecificOrigins);

app
    .UseStaticFiles() // For Dashboard.
    .UseHttpActivities()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
        endpoints.MapControllers();

        // For Dashboard.
        endpoints.MapFallbackToPage("/_Host");
    });

//app.MapGet("/", () => "Hello World!");

app.Run();
