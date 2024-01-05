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
    public class GetElectricity
    {
        public record Query() : IRequest<List<ElectricityDTO>>;

        public class Handler : IRequestHandler<Query, List<ElectricityDTO>>
        {
            private ICleanDbContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;
            public Handler(ICleanDbContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public Task<List<ElectricityDTO>> Handle(Query request, CancellationToken cancellationToken)
            {

                // var company = await _context.Companys.AsNoTracking().FirstOrDefaultAsync(x => x.Active == true);

                var elec = from ele in _context.Electricity.AsNoTracking()
                            orderby ele.ElectricityMonth descending

                            select new ElectricityDTO()
                            {
                                Id = ele.Id,
                                UnitElectricity = ele.UnitElectricity,
                                UnitUsed = Math.Max(0, (int)(ele.UnitElectricity * (ele.UnitUsed - _context.Electricity
                                            .Where(w => w.RoomId == ele.RoomId && w.ElectricityMonth < ele.ElectricityMonth)
                                            .OrderByDescending(w => w.ElectricityMonth)
                                            .Select(w => w.UnitUsed)
                                            .FirstOrDefault()))),



                                RoomId = ele.RoomId,
                                ElectricityMonth = ele.ElectricityMonth,
                            };


                return elec.ToListAsync();

            }
        }
    }
}
