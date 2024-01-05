using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Application.Interfaces;
using Domain.Configs;
using Domain.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static IdentityModel.ClaimComparer;

namespace Infrastructure.Services;

public class LineAuthService : ILineAuthService
{
    private readonly LineConfig _lineConfig;
    public LineAuthService(IOptions<LineConfig> options)
    {
        _lineConfig = options.Value;
    }

    public Task<string> GetAuthorizationPKCERequestURL(string code_verifier, string state, string returnUrl)
    {
        var code_challenge = string.Empty;
        using (var sha256 = SHA256.Create())
        {
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(code_verifier));
            code_challenge = Convert.ToBase64String(challengeBytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["response_type"] = "code";
        query["client_id"] = _lineConfig.ClientID;
        query["redirect_uri"] = getRedirectURL(returnUrl);
        //query["state"] = state;
        query["scope"] = "profile openid email";
        query["nonce"] = "09876xyz";
        var uri = new Uri("https://access.line.me/oauth2/v2.1/authorize");
        var builder = new UriBuilder(uri);
        builder.Port = -1;
        builder.Query = query.ToString();
        return Task.FromResult(builder.ToString());
    }

    public Task<string> GetAuthorizationRequestURL(string nonceid, string state, string returnUrl)
    {




        var query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("redirect_uri", _lineConfig.RedirectURL);
        query["response_type"] = "code";
        query["client_id"] = _lineConfig.ClientID;
        query["redirect_uri"] = getRedirectURL(returnUrl);
        query["state"] = state;
        query["scope"] = "profile openid email";
        query["nonce"] = nonceid;
        var uri = new Uri("https://access.line.me/oauth2/v2.1/authorize");
        var builder = new UriBuilder(uri);
        builder.Port = -1;
        builder.Query = query.ToString();
        return Task.FromResult(builder.ToString());
    }

    public async Task<AccessTokenLineDTO?> IssueAccessToken(string code, string code_verifier, string returnUrl)
    {
        using (var client = new HttpClient())
        {
            var data = new[] {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("code", code),
                                new KeyValuePair<string, string>("redirect_uri", getRedirectURL(returnUrl)),
                                new KeyValuePair<string, string>("client_id", _lineConfig.ClientID),
                                new KeyValuePair<string, string>("client_secret", _lineConfig.ClientSecret),
                                new KeyValuePair<string, string>("code_verifier", code_verifier)
                            };

            var response = await client.PostAsync("https://api.line.me/oauth2/v2.1/token", new FormUrlEncodedContent(data));
            
            string resultContent = await response.Content.ReadAsStringAsync();


            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return JsonConvert.DeserializeObject<AccessTokenLineDTO>(resultContent, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });


            //return JsonSerializer.Deserialize<AccessTokenLineDTO>(resultContent, options);
        }
    }

    public async Task<IDTokenPayloadDTO?> VerifyIDToken(string id_token)
    {
        var data = new[] {
                            new KeyValuePair<string, string>("id_token", id_token),
                            new KeyValuePair<string, string>("client_id", _lineConfig.ClientID),
                        };

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync("https://api.line.me/oauth2/v2.1/verify", new FormUrlEncodedContent(data));
            string resultContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IDTokenPayloadDTO>(resultContent);
        }
    }

    //public async Task<LineProfileDTO?> GetUserProfile(string access_token)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
    //        var result = await client.GetAsync("https://api.line.me/v2/profile");
    //        string resultContent = await result.Content.ReadAsStringAsync();
    //        return JsonConvert.DeserializeObject<LineProfileDTO>(resultContent);
    //    }
    //}

    private string getRedirectURL(string returnUrl)
    {
        //return url
        var returnUrlQuery = HttpUtility.ParseQueryString(string.Empty);
        if (!string.IsNullOrWhiteSpace(returnUrl)) returnUrlQuery.Add("return_url", returnUrl);

        var returnurl = new Uri(_lineConfig.RedirectURL).ToString();
        var returnUrlBuilder = new UriBuilder(returnurl);
        returnUrlBuilder.Port = -1;
        returnUrlBuilder.Query = returnUrlQuery.ToString();
        return returnUrlBuilder.ToString();
    }
}