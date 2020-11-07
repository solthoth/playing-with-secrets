using Microsoft.Extensions.Configuration;

namespace playing_with_secrets.tests
{
    public class TestConfiguration
    {
        public IConfiguration Configuration { get; set; }
        public TestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<TestConfiguration>();

            Configuration = builder.Build();
        }
    }
}
