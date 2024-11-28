using SignalR.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(action =>
{
	action.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("https://localhost:7099").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
	});
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapHub<MyHub>("/myhub");

app.UseAuthorization();

app.MapControllers();

app.Run();