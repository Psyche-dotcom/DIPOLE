using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Enitities;

namespace DipoleBank.Extension
{
    public static class DbRegisteredExtension
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DipoleBankContext>().AddDefaultTokenProviders();
            services.AddDbContext<DipoleBankContext>(dbContextOptions => dbContextOptions.UseSqlite(configuration["ConnectionStrings:DipoleConnectionString"]));
        }
    }
}
