using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace PGS.Azure.ActiveDirectory.Auth
{
    public static class AzureAdServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAzureAdOpenId(this IServiceCollection serviceCollection) =>
            serviceCollection.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, ConfigureAzureAdOpenIdOptions>();
    }
}