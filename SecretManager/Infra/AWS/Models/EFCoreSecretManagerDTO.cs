namespace Infra.AWS.Models;

public record EFCoreSecretManagerDTO(
	string Host,
	string Port,
	string Database,
	string Username,
	string Password);