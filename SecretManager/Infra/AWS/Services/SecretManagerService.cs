using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Infra.AWS.Interfaces;

namespace Infra.AWS.Services;

public class SecretManagerService : ISecretManagerService
{
	private readonly IAmazonSecretsManager _amazonSecretsManager;

	public SecretManagerService(IAmazonSecretsManager amazonSecretsManager)
	{
		_amazonSecretsManager = amazonSecretsManager;
	}

	public async Task<string> GetSecretAsync(string secretName, string region)
	{
		var getSecretValueRequest = new GetSecretValueRequest
		{
			SecretId = secretName,
		};

		var getSecretValueResponse = await _amazonSecretsManager.GetSecretValueAsync(getSecretValueRequest);

		var result = getSecretValueResponse.SecretString;

		if (result is null || string.IsNullOrWhiteSpace(result))
			throw new Exception("Secret string is null on AWS Secret Manager");

		return result;
	}
}