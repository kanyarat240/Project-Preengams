using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    
    public class EmailController : BaseController
    {

        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [AllowAnonymous]
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailTemplateDTO email)
        {
            if (email == null)
            {
                return BadRequest("ไม่พบข้อมูลอีเมล");
            }

           // var emailSender = new SendEmail(); // สร้างตัวแปร emailSender จากคลาส SendEmail
            bool isSent = await _emailService.SendEmailAsync(email); // เรียกใช้งานเมธอด SendEmailAsync จาก emailSender

            if (isSent)
            {
                return Ok("อีเมลถูกส่งสำเร็จ!");
            }
            else
            {
                return StatusCode(500, "เกิดข้อผิดพลาดในการส่งอีเมล");
            }
        }


        [AllowAnonymous]
        [HttpPost("email")]
        public async Task<IActionResult> testEmail([FromBody] EmailTemplateDTO email)
        {
            if (email == null)
            {
                return BadRequest("ไม่พบข้อมูลอีเมล");
            }

            // var emailSender = new SendEmail(); // สร้างตัวแปร emailSender จากคลาส SendEmail
            bool isSent = await _emailService.TestSendEmailAsync(email); // เรียกใช้งานเมธอด SendEmailAsync จาก emailSender

            if (isSent)
            {
                return Ok("อีเมลถูกส่งสำเร็จ!");
            }
            else
            {
                return StatusCode(500, "เกิดข้อผิดพลาดในการส่งอีเมล");
            }
        }
    }
}
