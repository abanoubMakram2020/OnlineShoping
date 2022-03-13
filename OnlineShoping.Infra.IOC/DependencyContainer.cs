using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShoping.Application.Automapper;
using OnlineShoping.Application.ServicesImplementation;
using OnlineShoping.Application.ServicesInterfaces;
using OnlineShoping.Application.UnitOfWork;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;
using OnlineShoping.Infra.Data.Repositrories;
using SharedKernal.Common.Configuration;
using SharedKernal.Middlewares.Basees;
using SharedKernal.Middlewares.JWTSettings;
using SharedKernal.Middlewares.Logging;
using SharedKernal.Middlewares.ResourcesReader.Message;

namespace OnlineShoping.Infra.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            #region DB Contexts
            services.AddDbContext<OnlineShopingDBContext>(options =>
            options.UseSqlServer(DatabaseConfiguration.OnlineShopDBConnection));


            #endregion

            AutoMapperConfiguration.RegisterMappings();
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            services.AddSingleton<ILogger, Logger>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();


            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IDashboardService, DashboardService>();





            services.AddScoped<IMessageResourceReader, MessageResourceReader>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<Presenter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
