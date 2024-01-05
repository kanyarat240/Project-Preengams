using System.Security.Cryptography;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Features.Auth;

public class GetLineLoginURL
{
    public record Query(string returnUrl) : IRequest<string>;

    public class Handler : IRequestHandler<Query, string>
    {
        private readonly ILineAuthService _lineAuthService;
        private readonly IMemoryCache _memoryCache;
        public Handler(ILineAuthService lineAuthService, IMemoryCache memoryCache)
        {
            _lineAuthService = lineAuthService;
            _memoryCache = memoryCache;
        }

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            // generate code_verifier
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[64];
            rng.GetBytes(bytes);

            // It is recommended to use a URL-safe string as code_verifier.
            // See section 4 of RFC 7636 for more details.
            var code_verifier = Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            var state = Guid.NewGuid().ToString();

            _memoryCache.Set(state, code_verifier, TimeSpan.FromHours(1));

            //return new AuthorizationRequestDTO() { URL = };
            return await _lineAuthService.GetAuthorizationRequestURL(code_verifier, state, request.returnUrl);
        }
    }
}

