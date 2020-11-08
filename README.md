# playing-with-secrets

## Overview
This is a sample project used to explore using [dotnet user secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows) as well as [Vault](https://www.vaultproject.io/)

## Prerequisits

* Docker & Docker-Compose
* RapidApi Account
    * Subscription to [Weather By Zipcode API](https://rapidapi.com/interzoid/api/us-weather-by-zip-code)

## Running the project

Start Vault dev server:
```
$ docker-compose up vault
```

Get Vault Token from output "Root Token":
```
vault_1  | You may need to set the following environment variable:
vault_1  |
vault_1  |     $ export VAULT_ADDR='http://0.0.0.0:8200'
vault_1  |
vault_1  | The unseal key and root token are displayed below in case you want to     
vault_1  | seal/unseal the Vault or re-authenticate.
vault_1  |
vault_1  | Unseal Key: rO9IQrQ+/W9rh1wg9jmDqqVp/u29aRevC/P4pwgbcd4=
vault_1  | Root Token: s.5iNQy4sXtVW6xYWj5jeJigKv
```

Create secret to store your RapidApi key
```
$ curl -H "X-Vault-Token: <TOKEN>" -X POST http://localhost:8200/v1/secret/data/rapidapi --data '{ "options": { "cas": 0 }, "data": { "Key": "Sample" } }'
```

Rename sample.env to Docker.env and update TOKEN value:
```
VaultOptions__Token=<TOKEN>
```

Build and run dotnet project:
```
$ docker-compose build api
$ docker-compose up api
```

Open browser and go to site:
```
http://localhost:5000
```