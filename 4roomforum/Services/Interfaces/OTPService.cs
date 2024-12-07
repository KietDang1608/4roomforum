using System;

namespace _4roomforum.Services.Interfaces;

public interface OTPService
{
    string GenerateAndStoreOTP(string email);

    bool ValidateOTP(string email, string otp);
}
