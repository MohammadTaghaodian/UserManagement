using Microsoft.EntityFrameworkCore;
using UserManagement;
using UserManagement.Dtos;
using UserManagement.entities;
using UserManagement.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContextPool<AppDbContext>( o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString(name: "DefultDataBase"));
});

builder.Services.AddScoped<IUserService, UserService>();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();


app.MapGet("hello",() => "Hello World");
app.MapGet("sayname",(string name) => "Hello "+name);

app.MapPost("user/Create", async (IUserService userService, UserCreateDto dto) =>
{
    UserResponse result = await userService.Create(dto);
    return Results.Ok(result);
});

app.MapGet("user/GetAll", async (IUserService userService) =>
{
    IEnumerable<UserResponse> result = await userService.GetAll();
    return Results.Ok(result);
});

app.MapGet("user/GetById/{id:guid}", async (IUserService userService, Guid id) =>
{
    UserResponse? result = await userService.GetById(id);
    return result == null ? Results.NotFound() : Results.Ok(result);
});

app.MapPut("user/Update", async (IUserService userService, UserUpdateDto dto) =>
{
    UserResponse? result = await userService.Update(dto);
    return result == null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("user/Delete{id:guid}", async (IUserService userService, Guid id) =>
{
    String result = await userService.Delete(id);
    return Results.Ok(result);
});

app.Run();
