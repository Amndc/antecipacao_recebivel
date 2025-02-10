using antecipacao_recebivel.Data;
using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Rules;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(); 
//builder.Services.AddScoped<ActionsEmpresa>();
//builder.Services.AddScoped<EmpresaRepo>();
builder.Services.AddControllers();

// Add services to the container.
//builder.Services.AddDbContext<DbContextRecebivel>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<DbContextRecebivel>(options => options.UseInMemoryDatabase("TestDb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Ativa o Swagger
    app.UseSwaggerUI(); // Ativa a interface do Swagger
}
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); // Habilitando as rotas da API
app.Run();
