using BetsAreMade.Repositories.NoSQL;
using BetsAreMade.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using BetsAreMade.DataContracts.Dbo.Users;
using BetsAreMade.DataContracts.Dto.Users;
using FluentValidation.AspNetCore;
using BetsAreMade.Controllers.Validator;

namespace BetsAreMade
{
    public partial class Startup
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

            services.AddSingleton<UserService>();
            services.AddSingleton<UserRepository>();

            services.AddAutoMapper((ca) => {
                ca.CreateMap<UserDbo, UserDto>().ReverseMap();
                ca.CreateMap<BetDbo, BetDto>().ReverseMap();
            });

            services.AddMvc()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<BetValidator>();
            });

            ConfigureMongoDb(services, Configuration);
            RegisterClassMap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
