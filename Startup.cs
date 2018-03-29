using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using webdev.DB;
using webdev.Repository;
using webdev.Repository.Implementation;
using webdev.Services;

namespace webdev
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Links API", Version = "v1" }));
			services.AddDbContext<LinkDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("LinksDbConnection")));
			services.AddTransient<ILinkDBRepository, LinkDBRepository>();
			services.AddSingleton<IHashService, HashService>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Links API"));

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Link}/{action=Index}");
			});

		}
	}
}