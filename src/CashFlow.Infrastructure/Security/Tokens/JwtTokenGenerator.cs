using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CashFlow.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : IAcessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey;
    
    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }
    
    public string Generate(User user)
    {
        // lista de claims para gerar o token com essas informacoes de base
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString())
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            // colocar as propriedades desejadas
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);  
    }

    private SymmetricSecurityKey SecurityKey()
    {
        // pegando os bytes da chave
        var key = Encoding.UTF8.GetBytes(_signingKey);
        
        return new SymmetricSecurityKey(key);
    }
}