using FluentAssertions;
using NSubstitute;
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
            var options = Substitute.For<IRapidApiOptions>();
            options.Key.Returns(testConfig.Configuration["RapidApiContext:Key"]);
            options.HostTemplate.Returns("https://{host}");
            options.WeatherByZipCode.Returns("us-weather-by-zip-code.p.rapidapi.com");
            
            repository = new WeatherByZipCodeRepository(options);
        }

        [Test]
        [Explicit("Cost money")]
        public async Task Given_ZipCode_When_Getting_Weather_Then_Returns_Result()
        {
            var zipcode = "94111";

            var result = await repository.GetByZipCode(zipcode);

            result.Should().NotBeNull();
        }
    }
}