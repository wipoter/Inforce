using BackEnd.Data;
using BackEnd.Extensions;
using BackEnd.Infrastructure;
using BackEnd.Mappers;
using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.BuilderConfigure;

public static class Services
{
    private static WebApplicationBuilder? _builder;
    private static IServiceCollection? _serviceCollection;

    public static void SetBuilder(WebApplicationBuilder? builder)
    {
        _builder = builder;
        _serviceCollection = builder?.Services;
    }

    public static void ServicesAdd()
    {
        if (_serviceCollection == null || _builder == null)
            return;
        _serviceCollection.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Дозволяємо доступ з localhost:3000
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        
        _serviceCollection.AddDbContext<ShortenerUrlContext>(options =>
        {
            options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        _serviceCollection.Configure<JwtOptions>(_builder.Configuration.GetSection(nameof(JwtOptions)));
        _serviceCollection.Configure<AuthorizationOptions>(_builder.Configuration.GetSection(nameof(AuthorizationOptions)));

        _serviceCollection.AddHttpContextAccessor();

        _serviceCollection.AddControllers();

        _serviceCollection.AddHttpContextAccessor();
        _serviceCollection.AddScoped<ILoginInfoRepository, LoginInfoRepository>();
        _serviceCollection.AddScoped<ILoginInfoService, LoginInfoService>();

        _serviceCollection.AddScoped<IUserRepository, UserRepository>();
        _serviceCollection.AddScoped<IUserService, UserService>();

        _serviceCollection.AddScoped<IUrlInfoRepository, UrlInfoRepository>();
        _serviceCollection.AddScoped<IUrlInfoService, UrlInfoService>();

        _serviceCollection.AddScoped<IJwtProvider, JwtProvider>();

        _serviceCollection.AddSingleton(DataBaseMapper.GetMapper());

        _serviceCollection.AddSwaggerGen();
        
        var jwtProvider = _serviceCollection.BuildServiceProvider().GetService<IJwtProvider>();
        if (jwtProvider != null) _serviceCollection.AddApiAuthentications(_builder.Configuration, jwtProvider);
    }
}