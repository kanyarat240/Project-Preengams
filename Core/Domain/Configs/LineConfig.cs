using System;
namespace Domain.Configs;

public class LineConfig
{
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectURL { get; set; }
    public string ChannelSecret { get; set; }
} 

