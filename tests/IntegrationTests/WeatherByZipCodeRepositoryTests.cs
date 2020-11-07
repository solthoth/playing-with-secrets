using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using playing_with_secrets.data;
using System.Threading.Tasks;

namespace playing_with_secrets.tests.IntegrationTests
{
    [TestFixture(Category = "IntegrationTests")]
    [Parallelizable]
    public class Tests
    {
        private WeatherByZipCodeRepository repository;
        [SetUp]
        public void Setup()
        {
            var testConfig = new TestConfiguration();
            var context = new RapidApiContext {
                Key = testConfig.Configuration["RapidApiContext:Key"],
                HostTemplate = "https://{host}",
                WeatherByZipCode = "us-weather-by-zip-code.p.rapidapi.com"
            };
            var options = Options.Create(context);
            repository = new WeatherByZipCodeRepository(options);
        }

        [Test]
        public async Task Given_ZipCode_When_Getting_Weather_Then_Returns_Result()
        {
            var zipcode = "94111";

            var result = await repository.GetByZipCode(zipcode);

            result.Should().NotBeNull();
        }
    }
}