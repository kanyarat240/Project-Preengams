using Application.Interfaces;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Application.Services
{
    public class SendEmail : IEmailService
    {
        private readonly EmailSettings _appSetting;
        private readonly ICleanDbContext _context;
        public SendEmail(IOptions<EmailSettings> options, ICleanDbContext context = null)
        {
            _appSetting = options.Value;
            _context = context;
        }
        public async Task<bool> SendEmailAsync(EmailTemplateDTO email)
        {
            try
            {
                // กำหนดข้อมูล SMTP Server
                SmtpClient smtpClient = new SmtpClient(_appSetting.Server , _appSetting.Port);
                smtpClient.UseDefaultCredentials = _appSetting.UseDefaultCredentials;
                smtpClient.EnableSsl = _appSetting.EnableSsl;

                // สร้างอีเมล
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_appSetting.EmailFrom); // อีเมลผู้ส่ง
                mailMessage.Subject = email.Subject; // หัวข้ออีเมล
                mailMessage.Body = email.Body; // เนื้อหาอีเมล
                mailMessage.IsBodyHtml = true; // ระบุว่าเนื้อหาเป็น HTML (ถ้าต้องการ)

                // เพิ่มผู้รับ
                mailMessage.To.Add(email.ToUser);

                // ตัวอย่างการเพิ่มผู้รับสำรอง (cc)
                if (!string.IsNullOrEmpty(email.Cc))
                {
                    mailMessage.CC.Add(email.Cc);
                }

                if (!string.IsNullOrEmpty(_appSetting.User))
                    smtpClient.Credentials = new NetworkCredential(_appSetting.User, _appSetting.Pass);

                // ส่งอีเมล
                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                // การจัดการข้อผิดพลาด
                Console.WriteLine("เกิดข้อผิดพลาดในการส่งอีเมล: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> TestSendEmailAsync(EmailTemplateDTO email)
        {
            try
            {
                // กำหนดข้อมูล SMTP Server
                SmtpClient smtpClient = new SmtpClient(_appSetting.Server, _appSetting.Port);
                smtpClient.UseDefaultCredentials = _appSetting.UseDefaultCredentials;
                smtpClient.EnableSsl = _appSetting.EnableSsl;

                // สร้างอีเมล
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_appSetting.EmailFrom);

                mailMessage.IsBodyHtml = true; // ระบุว่าเนื้อหาเป็น HTML (ถ้าต้องการ)
                //var emailContent = _context.Emails.FirstOrDefault(x => x.Id == email.Id);
                //mailMessage.Subject = emailContent.Subject;
                //mailMessage.Body = emailContent.Body;


                // เพิ่มผู้รับ
                mailMessage.To.Add(email.ToUser);

                // ตัวอย่างการเพิ่มผู้รับสำรอง (cc)
                if (!string.IsNullOrEmpty(email.Cc))
                {
                    mailMessage.CC.Add(email.Cc);
                }

                if (!string.IsNullOrEmpty(_appSetting.User))
                    smtpClient.Credentials = new NetworkCredential(_appSetting.User, _appSetting.Pass);

                // ส่งอีเมล
                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                // การจัดการข้อผิดพลาด
                Console.WriteLine("เกิดข้อผิดพลาดในการส่งอีเมล: " + ex.Message);
                return false;
            }
        }
    }
}
