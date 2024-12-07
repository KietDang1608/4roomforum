using System;

namespace _4roomforum.Services.Implements;

public class OtpGenerator
{
    public static string GenerateOTP(int length = 6)
    {
        var random = new Random();
        string otp = string.Empty;

        for (int i = 0; i < length; i++)
        {
            otp += random.Next(0, 10).ToString();
        }

        return otp;
    }
}
