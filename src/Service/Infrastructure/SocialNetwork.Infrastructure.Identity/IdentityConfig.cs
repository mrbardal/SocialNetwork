using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Infrastructure.Identity;

namespace SocialNetwork.Infrastructure.Persistance;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();


        services.AddScoped<JwtService>();

        services.AddAuthentication().AddIdentityCookies();

        return services;
    }
}
