using System;
using Microsoft.EntityFrameworkCore;

using PostService.Data;

namespace PostService;

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
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        
        // Thêm các service khác
        services.AddControllers();
        // Thêm các service cần thiết
        services.AddAutoMapper(typeof(Startup));

        services.AddScoped<IPostRepo, PostRepo>();  // Đăng ký CategoryRepo
        
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
