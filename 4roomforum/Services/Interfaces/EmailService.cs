using System;

namespace _4roomforum.Services.Interfaces;

public interface EmailService
{
    Task SendEmailAsync(string to, string subject, string body);

    Task SendOTPAsync(string to, string otp);

}
