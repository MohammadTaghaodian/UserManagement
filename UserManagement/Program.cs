using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using UserManagement;
using UserManagement.Dtos;
using UserManagement.entities;
using UserManagement.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.WriteIndented = false;

});

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
builder.Services.AddScoped<ISchoolService,SchoolService >();
builder.Services.AddScoped<IClassService, ClassService>();


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

//................ User

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

//................ School

app.MapPost("school/Create", async (ISchoolService schoolService, SchoolCreateDto dto) =>
{
    SchoolResponse result = await schoolService.Create(dto);
    return Results.Ok(result);
});

app.MapGet("school/GetAll", async (ISchoolService schoolService) =>
{
    IEnumerable<SchoolResponse> result = await schoolService.GetAll();
    return Results.Ok(result);
});

app.MapGet("school/GetById{id:guid}", async (ISchoolService schoolService, Guid id) =>
{
    SchoolResponse? result = await schoolService.GetById(id);
    return result == null ? Results.NotFound() : Results.Ok(result);
});

app.MapPut("school/Update", async (ISchoolService schoolService, SchoolUpdateDto dto) =>
{
    SchoolResponse? result = await schoolService.Update(dto);
    return result == null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("school/Delete{id:guid}", async (ISchoolService schoolService, Guid id) =>
{
    string result = await schoolService.Delete(id);
    return Results.Ok(result);
});

//................ Class

app.MapPost("class/Create", async (IClassService classService, ClassCreateDto dto) =>
{
    var result = await classService.Create(dto);
    return Results.Ok(result);
});

app.MapGet("class/GetAll", async (IClassService classService) =>
{
    var result = await classService.GetAll();
    return Results.Ok(result);
});

app.MapGet("class/GetById{id:guid}", async (IClassService classService , Guid id) =>
{
    var result = await classService.GetById(id);
    return Results.Ok(result);
});

app.MapPut("class/Update", async (IClassService classService, ClassUpdateDto classUpdateDto) =>
{
    var result = await classService.Update(classUpdateDto);
    return Results.Ok(result);
});

app.MapDelete("class/Delete{id:guid}", async (IClassService classService, Guid id) =>
{
    IResult result = await classService.Delete(id);
    return result;
});

app.Run();
