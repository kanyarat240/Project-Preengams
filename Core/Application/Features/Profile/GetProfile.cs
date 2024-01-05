using Application.Common.Enums;
using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Features.Profile;

public class GetProfile
{
    public record Query() : IRequest<UserProfileDTO?>;

    public class Handler : IRequestHandler<Query, UserProfileDTO?>
    {
        private ICleanDbContext _context;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        public Handler(ICleanDbContext context, ICurrentUserAccessor currentUserAccessor)
        {
            _context = context;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<UserProfileDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            var userAccount = await _context.UserAccount.AsNoTracking().FirstOrDefaultAsync(user => user.Id == _currentUserAccessor.UserId);
            var profile = await _context.Profile.AsNoTracking().FirstOrDefaultAsync(user => user.AccountId == _currentUserAccessor.UserId);
            
            //string base64 = Convert.ToBase64String(profile.Picture, 0, profile.Picture.Length);
            //var somesthing = "data:image/png;base64," + base64;
     


            if (userAccount is null)
                throw new AppException(ErrorMessage.Authentication.UserDoesNotExist);

            var result = new UserProfileDTO()
            {
                Id = profile.Id,
                Username = userAccount.Username,
                Firstname = profile.FirstName,
                Lastname = profile.LastName,
                Phone= profile.Phone,
                //Line = profile.LineAccountId,
                AddressDescription = profile.Address,
                AccountId = userAccount.Id,
                RoomId = profile.RoomId,
                IdCard = profile.IdCard,
                Password = userAccount.Password
                //Roles = userAccount.Roles,



            };

            //if(userAccount.Employee is not null)
            //{

            //    result.PictureUrl = userAccount.Employee.PictureURL;
            //    result.DisplayName = userAccount.Employee.FirstName + " " + userAccount.Employee.FirstName;
            //    result.InComplete = false;
            //}

            return result;
        }
    }
}

