namespace playing_with_secrets
{
    public class WeatherByZipCode
    {
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public decimal TempF { get; set; }
        public decimal TempC { get; set; }
        public string Weather { get; set; } = string.Empty;
        public decimal WindMPH { get; set; }
        public string WindDir { get; set; } = string.Empty;
        public decimal RelativeHumidity { get; set; }
        public decimal VisibilityMiles { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Credits { get; set; }
    }
}
