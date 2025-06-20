using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IArtifactService, ArtifactService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();