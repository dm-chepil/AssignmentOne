using AssignmentOne.Core.Interfaces;
using AssignmentOne.Core.Services;
using AssignmentOne.Infrastructure.Data;
using AssignmentOne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVideoGameRepository, VideoGameRepository>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
