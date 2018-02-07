using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace PGS.Azure.ActiveDirectory.Auth
{
    public class ConfigureAzureAdOpenIdOptions : IConfigureNamedOptions<OpenIdConnectOptions>
    {
        private readonly AzureAdOptions _azureOptions;

        public ConfigureAzureAdOpenIdOptions(IOptions<AzureAdOptions> options) => _azureOptions = options.Value;

        public void Configure(OpenIdConnectOptions options) => Configure(Options.DefaultName, options);

        public void Configure(string name, OpenIdConnectOptions options)
        {
            options.Authority = $"https://login.microsoftonline.com/{_azureOptions.Tenant}/v2.0";
            options.ClientId = _azureOptions.ClientId;
            options.CallbackPath = _azureOptions.CallbackPath;
        }
    }
}