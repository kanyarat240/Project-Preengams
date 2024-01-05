using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        //void SendEmail(EmailDTO request);
        Task<bool> SendEmailAsync(EmailTemplateDTO email);
        Task<bool> TestSendEmailAsync(EmailTemplateDTO email);
    }
}
