using System;
using System.Collections.Generic;

namespace Line.Messaging
{
    /// <summary>
    /// Short-lived channel access token that is valid for 30 days. 
    /// https://developers.line.me/en/docs/messaging-api/reference/#issue-channel-access-token
    /// </summary>
    public class ChannelAccessToken 
    {
        public string Scope { get; set; }
        public string AccessToken { get; set; }
        public string ClientId {  get; set; }
        public long ExpiresIn { get; set; }
        public DateTime ExpirationIn { get; set; }
        public string TokenType { get; } = "Bearer";
    }
}
