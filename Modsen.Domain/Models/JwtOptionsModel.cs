namespace Modsen.Domain.Models;

public class JwtOptionsModel
{
    public string SecretKey { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}