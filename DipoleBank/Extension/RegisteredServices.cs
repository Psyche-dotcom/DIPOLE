using Data.Repository.Implementation;
using Data.Repository.Interface;
using DipoleBank.Profiles;
using DipoleBank.Service.Implementation;
using DipoleBank.Service.Interface;

namespace DipoleBank.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddAutoMapper(typeof(ProjectProfile));
            services.AddScoped<IBankRepo, BankRepo>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBankAccountRepo, BankAccountRepo>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IEmailServices, EmailService>();
        }
    }
}
