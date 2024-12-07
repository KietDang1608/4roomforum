using System;
using System.Net;
using System.Net.Mail;
using _4roomforum.Services.Interfaces;
namespace _4roomforum.Services.Implements;

public class EmailServiceImpl : EmailService
{
    private readonly string _email = "csharphotel@gmail.com";
    private readonly string _password = "j2eePassword";
    private readonly string _host = "smtp.gmail.com";
    private readonly int _port = 587;


    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(_email, to, subject, body);
        var smtpClient = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_email, _password),
            EnableSsl = true
        };
        mailMessage.To.Add(to);
        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendOTPAsync(string to, string otp)
    {
        var subject = "Your OTP";
        var body = $"Your OTP is {otp}";
        await SendEmailAsync(to, subject, body);
    }
}
