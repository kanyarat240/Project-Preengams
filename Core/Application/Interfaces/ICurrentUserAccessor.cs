using System;
namespace Application.Interfaces;

public interface ICurrentUserAccessor
{
    int? UserId { get; }
    //string FullName { get; }
    //string Company { get; }
    //string Organization { get; }
    //string Language { get; }
    //string ProgramCode { get; }
}

