using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Vault;
using Vault.Models;

namespace playing_with_secrets.data
{
    public interface ISecretsRepository
    {
        Task<string> Get(string route, string key);
    }

    public class SecretsRepository : ISecretsRepository
    {
        private readonly IVaultClient VaultClient;
        private readonly ILogger<SecretsRepository> Logger;
        public SecretsRepository(ILogger<SecretsRepository> logger, IVaultClient vaultClient)
        {
            Logger = logger;
            VaultClient = vaultClient;
        }

        public async Task<string> Get(string route, string key)
        {
            var data = await GetData(route);
            data.TryGetValue(key, out var value);
            return value ?? string.Empty;
        }

        public async Task<Dictionary<string, string>> GetData(string route)
        {
            var secrets = await VaultClient.Secret.Read<Dictionary<string, Dictionary<string, string>>>(route) ?? new VaultResponse<Dictionary<string, Dictionary<string, string>>>();
            var value = GetData(secrets);
            return value ?? new Dictionary<string, string>();
        }

        private Dictionary<string, string> GetData(VaultResponse<Dictionary<string, Dictionary<string, string>>> secrets)
        {
            var data = secrets.Data ?? new Dictionary<string, Dictionary<string, string>>();
            if (secrets.Data == null)
            {
                Logger.LogDebug("Value warnings: {Warnings}", secrets.Warnings);
            }
            data.TryGetValue("data", out var value);
            return value ?? new Dictionary<string, string>();
        }
    }
}
