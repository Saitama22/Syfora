using Core.DI;
using Core.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

internal class Program {
	private static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
		//Добавление сервисов из проекта с логикой  
		builder.Services.AddServices(builder.Configuration);
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c => {
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
		});
		builder.Services.AddHttpClient();
		builder.Services.AddControllersWithViews();

		var app = builder.Build();		
		if (!app.Environment.IsDevelopment()) {
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		//Добавление сваггера, если требуется
		if (app.Configuration.GetValue<string>("EnableSwagger") == "true") {
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.RoutePrefix = string.Empty; // Открыть Swagger UI на корневом пути
			});
		}		

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();
		app.MapRazorPages();
		app.MapControllerRoute(
		   name: "default",
		   pattern: "{controller=Users}");
		app.Run();
	}
}