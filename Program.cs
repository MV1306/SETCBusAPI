using Microsoft.EntityFrameworkCore;
using SETCBusAPI.Data;
using SETCBusAPI.Methods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CommonServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
        //WithOrigins("https://localhost:44315")  // Allow the specific origin
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

builder.Services.AddDbContext<SETCDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
