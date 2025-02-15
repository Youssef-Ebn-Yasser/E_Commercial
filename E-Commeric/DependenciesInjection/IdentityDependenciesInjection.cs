namespace E_Commercial.DependenciesInjection;

public static class IdentityDependenciesInjection
{
    public static IServiceCollection AddIDensityDependenciesInjection(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
        {
            // password settings
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 6;

            // lock out setting
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.AllowedForNewUsers = true;

            // user setting
            opt.User.RequireUniqueEmail = true;

            //sign in
            //opt.SignIn.RequireConfirmedEmail = true;
            opt.SignIn.RequireConfirmedPhoneNumber = false;

        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        return services;
    }
}