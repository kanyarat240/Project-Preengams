using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs;

public class IDTokenPayloadDTO
{
    public string iss { get; set; }
    public string sub { get; set; }
    public int aud { get; set; }
    public int exp { get; set; }
    public int iat { get; set; }
    public List<string> amr { get; set; }
    public string name { get; set; }
    public string picture { get; set; }
    public string email { get; set; }
}

