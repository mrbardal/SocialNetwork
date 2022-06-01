using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Infrastructure.Identity;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.Infrastructure.Identity;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();


        services.AddScoped<JwtService>();

        services.AddAuthentication().AddIdentityCookies();

        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        //{
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ClockSkew = TimeSpan.Zero, // default: 5 min
        //        RequireSignedTokens = true,

        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,

        //        RequireExpirationTime = true,

        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = identitySettings.Issuer,
        //        ValidAudience = identitySettings.Audience,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.SecretKey)),

        //        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.Encryptkey))
        //    };
        //});

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        //}).AddJwtBearer(options =>
        //{
        //    var secretkey = Encoding.UTF8.GetBytes(identitySettings.SecretKey);
        //    var encryptionkey = Encoding.UTF8.GetBytes(identitySettings.Encryptkey);

        //    var validationParameters = new TokenValidationParameters
        //    {
        //        ClockSkew = TimeSpan.Zero, // default: 5 min
        //        RequireSignedTokens = true,

        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

        //        RequireExpirationTime = true,
        //        ValidateLifetime = true,

        //        ValidateAudience = true, //default : false
        //        ValidAudience = identitySettings.Audience,

        //        ValidateIssuer = true, //default : false
        //        ValidIssuer = identitySettings.Issuer,

        //        TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey),

        //    };

        //    options.RequireHttpsMetadata = false;
        //    options.SaveToken = true;
        //    options.TokenValidationParameters = validationParameters;
        //    options.Events = new JwtBearerEvents
        //    {
        //        OnAuthenticationFailed = context =>
        //        {
        //            return Task.CompletedTask;
        //        },
        //        OnTokenValidated = async context =>
        //        {
        //            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<AppUser>>();

        //            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        //            if (claimsIdentity.Claims?.Any() != true)
        //                context.Fail("This token has no claims.");

        //            var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
        //            if (!securityStamp.HasValue())
        //                context.Fail("This token has no secuirty stamp");

        //            //Find user and token from database and perform your custom validation
        //            var userId = claimsIdentity.GetUserId<int>();
        //            // var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

        //            //if (user.SecurityStamp != Guid.Parse(securityStamp))
        //            //    context.Fail("Token secuirty stamp is not valid.");

        //            var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
        //            if (validatedUser == null)
        //                context.Fail("Token secuirty stamp is not valid.");

        //        },
        //        OnChallenge = async context =>
        //        {
        //            //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //            //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

        //            /*
        //            if (context.AuthenticateFailure is SecurityTokenExpiredException)
        //            {
        //                context.HandleResponse();

        //                var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

        //                StringValues refreshToken;
        //                context.HttpContext.Request.Headers.TryGetValue("refresh_Token", out refreshToken);

        //                if (!refreshToken.Any())
        //                {
        //                    var response = new ApiResult(false,
        //                        ApiResultStatusCode.UnAuthorized, "Refresh Token Not Found");
        //                    context.Response.StatusCode = StatusCodes.Status424FailedDependency;
        //                    await context.Response.WriteAsJsonAsync(response);
        //                }
        //                else
        //                {
        //                    var newToken = await jwtService.RefreshToken(refreshToken.ToString());


        //                    if (newToken is null)
        //                    {
        //                        var failedResponse = new ApiResult(false,
        //                            ApiResultStatusCode.UnAuthorized, "Refresh Token Not Valid");
        //                        context.Response.StatusCode = StatusCodes.Status424FailedDependency;
        //                        await context.Response.WriteAsJsonAsync(failedResponse);

        //                    }

        //                    var response = new ApiResult<AccessToken>(true, ApiResultStatusCode.NotAcceptable, newToken);

        //                    context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
        //                    await context.Response.WriteAsJsonAsync(response);
        //                }
        //            }

        //            else if (context.AuthenticateFailure != null)
        //            {
        //                context.HandleResponse();

        //                var response = new ApiResult(false,
        //                    ApiResultStatusCode.UnAuthorized, "Token is Not Valid");
        //                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //                await context.Response.WriteAsJsonAsync(response);

        //            }
        //            */
        //        }
        //    };
        //});

        return services;
    }
}
