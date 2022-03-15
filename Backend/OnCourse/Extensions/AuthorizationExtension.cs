using Microsoft.Extensions.DependencyInjection;

namespace OnCourse.Extensions
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Adult", policy =>
                {
                    policy.RequireAssertion(request =>
                        request.User.HasClaim(claim =>
                            claim.Type == "Age" && int.Parse(claim.Value) >= 18
                        )
                    );
                });
            });

            return services;
        }
    }
}
