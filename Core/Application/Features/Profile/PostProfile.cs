using Application.Interfaces;
using Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile
{
    public class PostProfile
    {
        public class Command : IRequest<bool>
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Phone { get; set; }
            public string IdCard { get; set; }
            public string AddressDescription { get; set; }


        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private ICleanDbContext _context;
            private readonly IUserAccountService _userAccountService;

            public Handler(ICleanDbContext context, IUserAccountService userAccountService)
            {
                _context = context;
                _userAccountService = userAccountService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    TimeZoneInfo gmt7TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    if (request == null)
                    {
                        throw new ArgumentNullException("Data is null");
                    }
                    else
                    {
                        var Updatpro = _context.Profile.Where(t => t.Id == request.Id).FirstOrDefault();
                        if (Updatpro == null)
                        {
                            throw new ArgumentException("Data is null");
                        }
                        else
                        {
                            Updatpro.FirstName = request.Firstname;
                            Updatpro.LastName = request.Lastname;
                            Updatpro.Phone = request.Phone;
                            Updatpro.IdCard = request.IdCard;
                            Updatpro.Address = request.AddressDescription;
                            Updatpro.Active = true;


                            _context.Entry(Updatpro).State = EntityState.Modified;
                        }

                        //    TimeZoneInfo gmt7TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        //Domain.Entities.DB.Profiles profiles = new Domain.Entities.DB.Profiles
                        //{
                        //    Id = request.Id,
                        //    FirstName = request.Firstname,
                        //    LastName = request.Lastname,
                        //    Phone = request.Phone,
                        //    IdCard = request.IdCard,
                        //    Address = request.AddressDescription,
                        //    //AccountId = request.AccountId,
                        //    Active = true
                        //};

                        //_context.Entry(profiles).State = EntityState.Modified;

                        await _context.SaveChangesAsync();

                        return true;
                    }
                }
                catch (Exception e)
                {
                    // Log the exception
                    throw new ApplicationException("An error occurred while updating the team.", e);
                }

            }

        }
    }
}
