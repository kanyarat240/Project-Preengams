//using Application.Interfaces;
//using Domain.DTOs;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using System;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class testEmail //: IEmailService
//    {
//        private readonly EmailSettings _appSetting;
//        private ICleanDbContext _context;

//        public testEmail(IOptions<EmailSettings> options, ICleanDbContext context)
//        {
//            _appSetting = options.Value;
//            _context = context;
//        }

//        public async Task<bool> SendEmailAsync2(EmailTemplateDTO email)
//        {
//            try
//            {
//                // กำหนดข้อมูล SMTP Server
//                SmtpClient smtpClient = new SmtpClient(_appSetting.Server, _appSetting.Port);
//                smtpClient.UseDefaultCredentials = _appSetting.UseDefaultCredentials;
//                smtpClient.EnableSsl = _appSetting.EnableSsl;

//                // สร้างอีเมล
//                MailMessage mailMessage = new MailMessage();
//                mailMessage.From = new MailAddress(_appSetting.EmailFrom);
                
//                mailMessage.IsBodyHtml = true; // ระบุว่าเนื้อหาเป็น HTML (ถ้าต้องการ)
//                var emailContent = await _context.Emails.FirstOrDefaultAsync(x => x.Id == email.Id);
//                if (emailContent != null)
//                {
//                    mailMessage.Subject = emailContent.Subject;
//                    mailMessage.Body = emailContent.Body;

//                    // เพิ่มผู้รับ
//                    mailMessage.To.Add(email.ToUser);

//                    // โค้ดนี้จะเตรียมพร้อมสำหรับใช้งาน Cc จากฐานข้อมูล (ถ้ามี)
//                    string ccRecipients = emailContent.Cc;
//                    if (!string.IsNullOrEmpty(ccRecipients))
//                    {
//                        // สามารถแยกผู้รับสำรองหลายคนจาก Cc ในฐานข้อมูล
//                        var ccRecipientList = ccRecipients.Split(';');

//                        foreach (var ccRecipient in ccRecipientList)
//                        {
//                            mailMessage.CC.Add(ccRecipient.Trim());
//                        }
//                    }



//                    if (!string.IsNullOrEmpty(_appSetting.User))
//                        smtpClient.Credentials = new NetworkCredential(_appSetting.User, _appSetting.Pass);

//                    // ส่งอีเมล
//                    await smtpClient.SendMailAsync(mailMessage);

//                    return true;
//                }
//                else
//                {
//                    Console.WriteLine("ไม่พบข้อมูลอีเมลในฐานข้อมูล");
//                    return false;
//                }
//            }
//            catch (Exception ex)
//            {
//                // การจัดการข้อผิดพลาด
//                Console.WriteLine("เกิดข้อผิดพลาดในการส่งอีเมล: " + ex.Message);
//                return false;
//            }
//        }
//    }
//}
