using PokemonAPI.Services;
using PokemonAPI.utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuring Service
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("DB"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


//To be able to use the controllers
app.MapControllers();
app.Run();
