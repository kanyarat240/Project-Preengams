using System;
namespace Domain.DTOs;

public class AppSettingDTO
{
    public const string AppSettings = "AppSettings";

    public const string EmailSettings = "EmailSettings";
    //public EmailSettings? EmailSettings { get; set; }

}

public class EmailSettings
{
    public string? EmailFrom { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public bool EnableSsl { get; set; }
}

