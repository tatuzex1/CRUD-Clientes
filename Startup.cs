using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace crmall.Api
{
    using crmall.Api.Filters;
    using crmall.Services.Validators;
    using crmAll.IoC;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks();

            // Filtro de Action no MVC, pra validar o objeto antes de ir pras camadas de BusinessLogic, Repository.
            // Fail Fast Validation
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidationFilter));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClienteCommandValidator>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Adicionando a IHttpClientFactory para requisições externas
            services.AddHttpClient();

            services.AddMvc()
            .AddNewtonsoftJson(options =>
                        {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.Formatting = Formatting.None;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                options.UseCamelCasing(true);
              });




            // Registrando o DbContext nos Services.
            services.RegisterDataAccess(Configuration)
                .RegisterTransientServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
