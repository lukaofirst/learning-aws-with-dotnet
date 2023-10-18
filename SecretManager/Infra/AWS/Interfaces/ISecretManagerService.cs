namespace Infra.AWS.Interfaces;

public interface ISecretManagerService
{
	Task<string> GetSecretAsync(string secretName, string region);
}