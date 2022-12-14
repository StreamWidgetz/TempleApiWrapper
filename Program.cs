using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(opt =>
{
    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.UseHttpsRedirection();

app.MapGet("/competition/{id:int}", async ([FromRoute]int id, HttpClient client) =>
{
    var res = await client.GetAsync($"https://templeosrs.com/api/competition_info.php?id={id}");

    var str = await res.Content.ReadAsStringAsync();

    return str;
})
.WithName("GetCompetition");

app.Run();


