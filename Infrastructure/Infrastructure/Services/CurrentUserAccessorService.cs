using System;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CurrentUserAccessorService : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int? _userId { set; get; }

        public CurrentUserAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            string user_id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(user_id))
            {
                _userId = int.Parse(user_id);
            }
        }

        public int? UserId
        {
            get => _userId;
        }
    }
}

