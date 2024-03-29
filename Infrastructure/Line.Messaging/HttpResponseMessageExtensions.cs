﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Line.Messaging
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Validate the response status.
        /// </summary>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                ErrorResponseMessage errorMessage = new ErrorResponseMessage();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    errorMessage = JsonConvert.DeserializeObject<ErrorResponseMessage>(content);
                    
                }
                catch
                {
                    errorMessage = new ErrorResponseMessage() { Message = content, Details = new ErrorResponseMessage.ErrorDetails[0] };
                }
                throw new LineResponseException(errorMessage.Message) { StatusCode = response.StatusCode, ResponseMessage = errorMessage };
            }
        }
    }
}
