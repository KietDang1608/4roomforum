using System;
using MicroServices.UserServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserServices.Data;
namespace UserServices;
using AutoMapper;
public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    
    {
        services.AddDbContext<AppDBContext>(options =>
            options.UseMySql(
                _configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 23)) // Thay bằng phiên bản MySQL bạn đang sử dụng
            ));

        // Thêm các service khác
        //services.AddControllers();
        // Thêm các service cần thiết
        services.AddControllers();  // Thêm hỗ trợ Web API
        services.AddAutoMapper(typeof(Startup));

        services.AddScoped<IRoleRepo, RoleRepo>();
        services.AddScoped<IUserRepo, UserRepo>();// Đăng ký CategoryRepo
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseHttpsRedirection();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();  // Cấu hình API endpoints
        });
    }
}
