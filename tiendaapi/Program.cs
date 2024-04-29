var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app => {

        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();   
    });
});
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");  
app.UseAuthorization();
app.MapControllers();
app.Run();
