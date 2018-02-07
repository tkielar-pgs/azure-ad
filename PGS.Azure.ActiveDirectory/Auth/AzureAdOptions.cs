namespace PGS.Azure.ActiveDirectory.Auth
{
    public class AzureAdOptions
    {
        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string CallbackPath { get; set; }
    }
}