using Data.util.Cryptography;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Configuration;
using Services.DTO;

namespace Services.Auth;

public interface IAuthServices
{
    string  GetJwtToken(JwtParam param);
    bool Validate(string providedPassword, string savedPassword);
}

public class AuthServices : IAuthServices
{
    private readonly IPasswordHasher _passwordHasher;
    private string _JWTSecret;
    private int _JWTExpiryDays;

    public AuthServices(IPasswordHasher passwordHasher,IConfiguration configuration)
    {
        _passwordHasher = passwordHasher;
        _JWTSecret = configuration.GetValue("JWTSecret", string.Empty);
        _JWTExpiryDays = configuration.GetValue("JWTExpiryDays", 1);
    }
    public string  GetJwtToken(JwtParam param)
    {
        return JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(_JWTSecret) 
            .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(_JWTExpiryDays).ToUnixTimeSeconds()) 
            .AddClaim("UserId", param.UserId)
            .AddClaim("Name", param.Name)
            .AddClaim("Email", param.Email)
            .AddClaim("IssuedAt", DateTimeOffset.Now.ToUnixTimeMilliseconds())
            .Encode();
    }
    
    public  bool Validate(string providedPassword, string savedPassword)
    {
        var (verified, _) = _passwordHasher.Check(savedPassword, providedPassword);

        return verified;
    }
}