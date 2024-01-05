using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StatusEnum;

namespace Application.Features.Room
{
    public class GetRoom
    {
        public record Query() : IRequest<List<RoomDTO>>;

        public class Handler : IRequestHandler<Query, List<RoomDTO>>
        {
            private ICleanDbContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;
            public Handler(ICleanDbContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public Task<List<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
            {

                // var company = await _context.Companys.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);

                var room = from rooms in _context.Room.AsNoTracking()
                           where rooms.Active == false

                           select new RoomDTO()
                             {
                                 Id = rooms.Id,
                                 TypeRoom = rooms.TypeRoom,
                                 PiceRoom = rooms.PiceRoom,
                                 Active = rooms.Active,
                             };


                return room.ToListAsync();

            }
        }
    }
}
