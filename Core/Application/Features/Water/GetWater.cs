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
    public class GetWater
    {
        public record Query() : IRequest<List<WaterDTO>>;

        public class Handler : IRequestHandler<Query, List<WaterDTO>>
        {
            private ICleanDbContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;
            public Handler(ICleanDbContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public Task<List<WaterDTO>> Handle(Query request, CancellationToken cancellationToken)
            {

                // var company = await _context.Companys.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);

                var water = from waters in _context.Water.AsNoTracking()
                            orderby waters.WaterMonth descending

                            select new WaterDTO()
                           {
                               Id = waters.Id,
                               UnitWater = waters.UnitWater,
                               UnitUsed = Math.Max(0, (int)(waters.UnitWater * (waters.UnitUsed - _context.Water
                                            .Where(w => w.RoomId == waters.RoomId && w.WaterMonth < waters.WaterMonth)
                                            .OrderByDescending(w => w.WaterMonth)
                                            .Select(w => w.UnitUsed)
                                            .FirstOrDefault()))),



                                RoomId = waters.RoomId,
                               WaterMonth = waters.WaterMonth,
                           };


                return water.ToListAsync();

            }
        }
    }
}
