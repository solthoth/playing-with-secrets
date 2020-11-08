namespace playing_with_secrets.data
{
    public interface IRapidApiSecrets
    {
        string Key { get; }
    }

    public class RapidApiSecrets : IRapidApiSecrets
    {
        private readonly ISecretsRepository secretsRepository;
        public RapidApiSecrets(ISecretsRepository repository)
        {
            secretsRepository = repository;
        }

        public string Key => secretsRepository.Get("/secret/data/rapidapi", nameof(Key)).Result;
    }
}
