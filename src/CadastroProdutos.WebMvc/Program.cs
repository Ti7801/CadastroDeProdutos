using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Services;
using BibliotecaData.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(
    (DbContextOptionsBuilder optionsBuilder) =>
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    },
    ServiceLifetime.Scoped
);

builder.Services.AddScoped<IProdutoRepository, ProdutoRespository>();
builder.Services.AddScoped<CadastrarProdutoServices>();
builder.Services.AddScoped<AtualizarProdutoServices>();
builder.Services.AddScoped<ListarProdutoServices>();
builder.Services.AddScoped<ExcluirProdutoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produto}/{action=Index}/{id?}");

app.Run();
