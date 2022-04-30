namespace Meals.API.Keysettings;

public class ApiKeyConfig
{
    public string? SecretForKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}