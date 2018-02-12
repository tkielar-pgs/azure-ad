using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
            options.ResponseType = OpenIdConnectResponseType.IdTokenToken;
            options.Scope.Add("Directory.Read.All");

            options.Events = new OpenIdConnectEvents
            {
                OnTokenValidated = async context =>
                {
                    var graph = new GraphServiceClient(new DelegateAuthenticationProvider(message =>
                    {
                        message.Headers.Authorization = 
                            new AuthenticationHeaderValue(context.ProtocolMessage.TokenType, context.ProtocolMessage.AccessToken);
                        return Task.CompletedTask;
                    }));

                    IGraphServiceGroupsCollectionPage groupsResponse = 
                        await graph.Groups.Request().Filter("displayName eq 'Administrators'").GetAsync();

                    IDirectoryObjectCheckMemberGroupsCollectionPage membershipResponse = 
                        await graph.Me.CheckMemberGroups(groupsResponse.Select(group => group.Id)).Request().PostAsync();

                    if (membershipResponse.Count == groupsResponse.Count)
                    {
                        context.Principal.AddIdentity(new ClaimsIdentity(new[] {new Claim(ClaimTypes.Role, "Administrator")}));
                    }
                }
            };
        }
    }
}