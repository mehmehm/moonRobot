using robot_controller_api.persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandADO>();
//builder.Services.AddScoped<IMapDataAccess, MapADO>();

builder.Services.AddScoped<IRobotCommandDataAccess,RobotCommandRepository>();
builder.Services.AddScoped<IMapDataAccess, MapRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
