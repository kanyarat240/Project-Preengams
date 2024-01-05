using System;
using System.Text.Json;

namespace Domain.DTOs;

public class ErrorMessageDTO
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    //public override string ToString()
    //{
    //    return JsonSerializer.Serialize(this);
    //}
}

