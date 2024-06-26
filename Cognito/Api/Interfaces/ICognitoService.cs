using Amazon.CognitoIdentityProvider.Model;
using Api.Models;

namespace Api.Interfaces
{
    public interface ICognitoService
    {
        Task<SignUpResponse> Register(Person person);
        Task<ConfirmSignUpResponse> ConfirmRegistration(ConfirmRegistration confirmRegistration);
        Task<AdminInitiateAuthResponse> Login(Login login);
    }
}
