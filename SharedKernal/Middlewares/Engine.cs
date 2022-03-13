using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedKernal.Common.Configuration;
using SharedKernal.Common.Enum;
using SharedKernal.Middlewares.Exception;
using SharedKernal.Middlewares.JWTSettings;
using SharedKernal.Middlewares.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Middlewares
{
    public static class Engine
    {
        public static IServiceProvider Container { get; set; }
        public static string[] AllowedOrigins { get; set; }

        public static void Initialize(this IConfiguration configuration)
        {
            var _databaseConfiguration = new DatabaseConfiguration();
            configuration.Bind(nameof(DatabaseConfiguration), _databaseConfiguration);

            var _swaggerSettings = new SwaggerSettings();
            configuration.Bind(nameof(SwaggerSettings), _swaggerSettings);


            var _jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), _jwtSettings);
            
            var _uploadFilesConfigurations = new UploadFilesConfigurations();
            configuration.Bind(nameof(UploadFilesConfigurations), _uploadFilesConfigurations);


            IConfigurationSection originsSection = configuration.GetSection("AllowedOrigins");
            AllowedOrigins = originsSection.AsEnumerable().Where(s => s.Value != null).Select(a => a.Value).ToArray();
        }

        public static void Initialize(this IServiceCollection service)
        {
            #region App localization
            service.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(nameof(LanguageLetter.Ar).ToLower());
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo(nameof(LanguageLetter.En).ToLower()), new CultureInfo(nameof(LanguageLetter.Ar).ToLower()) };

                options.RequestCultureProviders.Insert(0, new Microsoft.AspNetCore.Localization.CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? nameof(LanguageLetter.Ar).ToLower() : firstLang;
                    return Task.FromResult(new Microsoft.AspNetCore.Localization.ProviderCultureResult(defaultLang, defaultLang));
                }));
            });
            #endregion
            #region Authentication
            service.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = JwtSettings.RequireExpirationTime,
                    ValidateIssuer = JwtSettings.ValidateIssuer,
                    ValidateAudience = JwtSettings.ValidateAudience,
                    ValidAudience = JwtSettings.ValidAudience,
                    ValidIssuer = JwtSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecurityKey))
                };
            });

            #endregion
            service.AddSwaggerDocumentation(documentName: SwaggerSettings.Name, title: SwaggerSettings.Title, version: SwaggerSettings.Version, description: SwaggerSettings.Description);
            service.AddMemoryCache();
 
            service.AddHttpContextAccessor();
        }

        public static void Initialize(this IApplicationBuilder app)
        {
            Container = app.ApplicationServices;

            app.UseSwaggerDocumentation(documentName: SwaggerSettings.Name, title: SwaggerSettings.Title, version: SwaggerSettings.Version);
            app.UseMiddleware<ExceptionHandler>();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }

}