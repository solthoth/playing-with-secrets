using Microsoft.Extensions.Options;

namespace playing_with_secrets.data
{
    public interface IRapidApiOptions
    {
        string Key { get; set; } 
        string HostTemplate { get; set; } 
        string WeatherByZipCode { get; set; } 
    }
    public class RapidApiOptions : RapidApiContext, IRapidApiOptions
    {
        private readonly RapidApiContext rapidApiContext;
        private readonly IRapidApiSecrets rapidApiSecrets;

        public RapidApiOptions(IOptions<RapidApiContext> options, IRapidApiSecrets secrets)
        {
            rapidApiContext = options.Value;
            rapidApiSecrets = secrets;
        }

        public override string Key => string.IsNullOrEmpty(rapidApiContext.Key) ? rapidApiSecrets.Key : rapidApiContext.Key;
        public override string HostTemplate => rapidApiContext.HostTemplate;
        public override string WeatherByZipCode => rapidApiContext.WeatherByZipCode;
    }
}
