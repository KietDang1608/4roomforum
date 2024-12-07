using System;
using Microsoft.Extensions.Caching.Memory;
using _4roomforum.Services.Interfaces;
namespace _4roomforum.Services.Implements;

public class OTPServiceImpl : OTPService
{
    private readonly IMemoryCache _cache;

    public OTPServiceImpl(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string GenerateAndStoreOTP(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();

        // Store OTP with expiration
        var cacheKey = $"OTP_{email}";
        _cache.Set(cacheKey, otp, TimeSpan.FromMinutes(1)); // OTP expires in 1 minute

        return otp;
    }

    public bool ValidateOTP(string email, string inputOtp)
    {
        var cacheKey = $"OTP_{email}";

        // Try to get the OTP from cache
        if (_cache.TryGetValue(cacheKey, out string storedOtp))
        {
            if (storedOtp == inputOtp)
            {
                // OTP is valid; remove it from the cache
                _cache.Remove(cacheKey);
                return true;
            }
        }
        return false;
    }

}
