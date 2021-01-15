using AutoMapper;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Web.Services;
using Hahn.ApplicatonProcess.December2020.Web.Utils;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.December2020.Web
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
			services.AddData(Configuration["DbConnectionString"]);
			services.AddHttpClient<CountryService>();
			services.AddTransient<IApplicationHttpContextService, ApplicationHttpContextService>();

			services.AddAutoMapper(typeof(Startup));
			services.AddMediatR(typeof(Startup));
			services.AddHttpContextAccessor();
			
			services.AddControllers()
				.AddFluentValidation(opts => opts.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

			services.AddCors(options => options.AddPolicy("AllowAll", builder =>
			{
				builder.AllowAnyOrigin();
				builder.AllowAnyMethod();
				builder.AllowAnyHeader();
			}));

			services.Configure<ApiBehaviorOptions>(options =>
				options.InvalidModelStateResponseFactory = InvalidModelStateResponseHandler.Handle);

			services.AddSwaggerGen(c =>
			{
				c.CustomSchemaIds(type => type.ToString());
				c.ExampleFilters();
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.December2020.Web", Version = "v1" });
			});

			services.AddSwaggerExamplesFromAssemblyOf<Startup>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseCors("AllowAll");
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1"));
			}

			app.UseExceptionHandler("/exception");
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
