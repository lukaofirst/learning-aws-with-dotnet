using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Api.Interfaces;
using Api.Models;

namespace Api.Services
{
    public class CognitoService(
        IAmazonCognitoIdentityProvider amazonCognitoIdentityProvider,
        IConfiguration configuration) : ICognitoService
    {
        private readonly IAmazonCognitoIdentityProvider _amazonCognitoIdentityProvider = amazonCognitoIdentityProvider;
        private readonly IConfiguration _configuration = configuration;

        public async Task<SignUpResponse> Register(Person person)
        {
            var clientId = _configuration.GetValue<string>("AWS:ClientId");

            var signUpRequest = new SignUpRequest
            {
                ClientId = clientId,
                Username = person.Email,
                Password = person.Password,
                UserAttributes =
                [
                    new() { Name = "email", Value = person.Email },
                    new() { Name = "name", Value = person.Name },
                    new() { Name = "middle_name", Value = person.MiddleName }
                ]
            };

            var response = await _amazonCognitoIdentityProvider.SignUpAsync(signUpRequest);
            return response;
        }

        public async Task<ConfirmSignUpResponse> ConfirmRegistration(ConfirmRegistration confirmRegistration)
        {
            var clientId = _configuration.GetValue<string>("AWS:ClientId");

            var confirmRequest = new ConfirmSignUpRequest
            {
                ClientId = clientId,
                Username = confirmRegistration.Email,
                ConfirmationCode = confirmRegistration.Code
            };

            var response = await _amazonCognitoIdentityProvider.ConfirmSignUpAsync(confirmRequest);
            return response;
        }

        public async Task<AdminInitiateAuthResponse> Login(Login login)
        {
            var clientId = _configuration.GetValue<string>("AWS:ClientId");
            var userPoolId = _configuration.GetValue<string>("AWS:UserPoolId");

            var authParameters = new Dictionary<string, string>
            {
                { "USERNAME", login.Email ?? string.Empty },
                { "PASSWORD", login.Password ?? string.Empty }
            };

            var request = new AdminInitiateAuthRequest
            {
                ClientId = clientId,
                UserPoolId = userPoolId,
                AuthParameters = authParameters,
                AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH
            };

            var response = await _amazonCognitoIdentityProvider.AdminInitiateAuthAsync(request);
            return response;
        }
    }
}
