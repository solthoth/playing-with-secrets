using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using playing_with_secrets.data;
using System.Threading.Tasks;
using Vault;

namespace playing_with_secrets.tests.IntegrationTests
{
    [TestFixture]
    [Parallelizable]
    public class SecretsRepositoryTests
    {
        private SecretsRepository repository;

        [SetUp]
        public void SetUp()
        {
            var testConfig = new TestConfiguration();
            var logger = Substitute.For<ILogger<SecretsRepository>>();
            var options = Options.Create(new VaultOptions { 
                Address = "http://localhost:8200/",
                Token = testConfig.Configuration["VAULT_TOKEN"]
            });
            var vaultClient = new VaultClient(options);
            repository = new SecretsRepository(logger, vaultClient);
        }

        [TestCase("/secret/data/rapidapi")]
        public async Task Given_Route_When_Getting_Key_Then_Returns_NonEmpty_String(string value)
        {
            var key = await repository.Get(value, "Key");
            key.Should().NotBeNullOrEmpty();
        }

        [TestCase("/secret/data/unknown")]
        public async Task Given_Route_When_Getting_Key_Then_Returns_Empty_String(string value)
        {
            var key = await repository.Get(value, "Key");
            key.Should().BeEmpty();
        }
    }
}
