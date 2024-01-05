using Domain.DTOs;

namespace Application.Interfaces;

public interface ILineAuthService
{
    Task<string> GetAuthorizationPKCERequestURL(string code_verifier, string state, string returnUrl);
    Task<string> GetAuthorizationRequestURL(string nonceid, string state, string returnUrl);
    Task<AccessTokenLineDTO?> IssueAccessToken(string code, string code_verifier, string returnUrl);
    Task<IDTokenPayloadDTO?> VerifyIDToken( string id_token);
    //Task<LineProfileDTO?> GetUserProfile(string access_token);
}