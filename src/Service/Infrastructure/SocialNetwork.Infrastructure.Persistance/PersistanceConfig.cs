using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SocialNetwork.Domain.Aggregates.FriendshipAggregate;
using SocialNetwork.Infrastructure.Identity;
using SocialNetwork.Infrastructure.Persistance.Core;
using SocialNetwork.Infrastructure.Persistance.Data;
using SocialNetwork.Infrastructure.Persistance.Repositories;

namespace SocialNetwork.Infrastructure.Persistance;

public static class PersistanceConfig
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppContextDb>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SocialNetwork"));
        });

        services.AddIdentityCore<AppUser>(options =>
        {
            options.Stores.ProtectPersonalData = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = false;

            options.User.RequireUniqueEmail = false;
        })
        .AddSignInManager<SignInManager<AppUser>>()
        .AddEntityFrameworkStores<AppContextDb>();

        services.TryAddSingleton<ISystemClock, SystemClock>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFriendshipRepository, FriendshipRepository>();

        return services;
    }
}
