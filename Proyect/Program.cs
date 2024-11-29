using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Proyect.Models;
using Proyect.Servicios.Contrato;
using Proyect.Servicios.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Proyect.Validaciones;

namespace Proyecto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProyectContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
            });

            builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

            builder.Services.AddScoped<IClienteService, ClienteService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(option =>
              {
                  option.LoginPath = "/Inicio/Login";
                  option.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                  option.AccessDeniedPath = "/Inicio/Error";
              });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

            app.Run();
        }
    }
}
